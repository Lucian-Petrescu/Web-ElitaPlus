Option Explicit On
Option Strict On
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading

Namespace Tables
    Partial Class ClaimSystemForm
        Inherits ElitaPlusSearchPage

        Private Class PageStatus

            Public Sub New()
                pageIndex = 0
                pageCount = 0
            End Sub

        End Class
#Region "Member Variables"

        Private Shared pageIndex As Integer
        Private Shared pageCount As Integer

#End Region

#Region "Properties"

        Public ReadOnly Property IsEditing() As Boolean
            Get
                IsEditing = (grdView.EditIndex > NO_ROW_SELECTED_INDEX)
            End Get
        End Property

#End Region

#Region "Page State"
        Class MyState
            Public PageIndex As Integer = 0
            Public MyBO As ClaimSystem
            Public DescriptionMask As String
            Public UseAccountingMask As String
            Public ClaimSystemId As Guid
            Public Id As Guid
            Public IsGridVisible As Boolean
            Public IsAfterSave As Boolean
            Public IsEditMode As Boolean
            Public AddingNewRow As Boolean
            Public Canceling As Boolean
            Public searchDV As DataView = Nothing
            Public SortExpression As String = ClaimSystem.ClaimSystemDV.COL_DESCRIPTION
            Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
            Public bnoRow As Boolean = False
            Public YESNOdv As DataView
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
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


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Constants"

        Private Const ID_COL As Integer = 2
        Private Const CODE_COL As Integer = 3
        Private Const DESCRIPTION_COL As Integer = 4
        Private Const NEW_CLAIM_COL As Integer = 5
        Private Const PAY_CLAIM_COL As Integer = 6
        Private Const MAINTAIN_CLAIM_COL As Integer = 7

        Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

        Private Const DESCRIPTION_CONTROL_NAME As String = "DescriptionTextBox"
        Private Const CODE_CONTROL_NAME As String = "CodeTextBox"
        Private Const ID_CONTROL_NAME As String = "IdLabel"
        Private Const NEW_CLAIM_CONTROL_NAME As String = "NewClaimDropdown"
        Private Const PAY_CLAIM_CONTROL_NAME As String = "PayClaimDropdown"
        Private Const MAINTAIN_CLAIM_CONTROL_NAME As String = "MaintainClaimDropDown"
        Private Const NEW_CLAIM_LABEL_CONTROL_NAME As String = "NewClaimLabel"
        Private Const PAY_CLAIM_LABEL_CONTROL_NAME As String = "PayClaimLabel"
        Private Const MAINTAIN_CLAIM_LABEL_CONTROL_NAME As String = "MaintainClaimLabel"
        Private Const DESCRIPTION_LABEL_CONTROL_NAME As String = "DescriptionLabel"
        Private Const CODE_LABEL_CONTROL_NAME As String = "CodeLabel"

        Private Const EDIT_COMMAND As String = "EditRecord"
        Private Const DELETE_COMMAND As String = "DeleteRecord"
        Private Const SORT_COMMAND As String = "Sort"

        Private Const YESNO As String = "YESNO"

        Private Const NO_ROW_SELECTED_INDEX As Integer = -1

        Public Const PAGETITLE As String = "CLAIM_SYSTEM"
        Public Const PAGETAB As String = "Admin"

#End Region

#Region "Button Click Handlers"

        Private Sub SearchButton_Click(sender As System.Object, e As System.EventArgs) Handles SearchButton.Click
            Try
                State.IsGridVisible = True
                State.searchDV = Nothing
                PopulateGrid()
                'Me.State.PageIndex = Me.grdView.CurrentPageIndex
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub ClearButton_Click(sender As System.Object, e As System.EventArgs) Handles ClearButton.Click

            Try
                ClearSearchCriteria()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub ClearSearchCriteria()

            Try
                SearchDescriptionTextBox.Text = String.Empty

                'Update Page State
                With State
                    .DescriptionMask = SearchDescriptionTextBox.Text
                End With
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub NewButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles NewButton_WRITE.Click

            Try
                State.IsEditMode = True
                State.IsGridVisible = True
                State.AddingNewRow = True
                AddNew()
                SetButtonsState()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub SaveButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles SaveButton_WRITE.Click

            Try

                AssignBOFromSelectedRecord()
                If (State.MyBO.IsDirty) Then
                    State.MyBO.Save()
                    State.IsAfterSave = True
                    State.AddingNewRow = False
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

        Private Sub CancelButton_Click(sender As System.Object, e As System.EventArgs) Handles CancelButton.Click

            Try
                grdView.SelectedIndex = NO_ITEM_SELECTED_INDEX
                State.Canceling = True
                If (State.AddingNewRow) Then
                    State.AddingNewRow = False
                    State.searchDV = Nothing
                End If
                ReturnFromEditing()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

#End Region


#Region "Private Methods"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

            'Put user code to initialize the page here
            Try
                ErrControllerMaster.Clear_Hide()

                SetFormTitle(PAGETITLE)
                SetFormTab(PAGETAB)

                SetStateProperties()
                If Not Page.IsPostBack Then
                    SortDirection = ClaimSystem.ClaimSystemDV.COL_DESCRIPTION
                    SetDefaultButton(SearchDescriptionTextBox, SearchButton)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    SetGridItemStyleColor(grdView)
                    State.PageIndex = 0
                    If State.MyBO Is Nothing Then
                        State.MyBO = New ClaimSystem
                    End If
                    SetButtonsState()
                    TranslateGridHeader(grdView)
                    TranslateGridControls(grdView)
                    PopulateAll()
                Else
                    CheckIfComingFromDeleteConfirm()
                End If
                BindBoPropertiesToGridHeaders()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)
        End Sub

        Private Sub PopulateAll()
            Dim YESNOdv As DataView = LookupListNew.DropdownLookupList(YESNO, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
            State.YESNOdv = YESNOdv

        End Sub

        Private Sub PopulateGrid()

            Dim dv As DataView

            Try

                If (State.searchDV Is Nothing) Then
                    State.searchDV = GetDV()
                End If
                State.searchDV.Sort = State.SortExpression

                If (State.IsAfterSave) Then
                    State.IsAfterSave = False
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.Id, grdView, State.PageIndex)
                ElseIf (State.IsEditMode) Then
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.Id, grdView, State.PageIndex, State.IsEditMode)
                Else
                    SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, grdView, State.PageIndex)
                End If

                grdView.AutoGenerateColumns = False
                grdView.Columns(CODE_COL).SortExpression = ClaimSystem.ClaimSystemDV.COL_CODE
                grdView.Columns(DESCRIPTION_COL).SortExpression = ClaimSystem.ClaimSystemDV.COL_DESCRIPTION
                grdView.Columns(NEW_CLAIM_COL).SortExpression = ClaimSystem.ClaimSystemDV.COL_NEW_CLAIM
                grdView.Columns(PAY_CLAIM_COL).SortExpression = ClaimSystem.ClaimSystemDV.COL_PAY_CLAIM
                grdView.Columns(MAINTAIN_CLAIM_COL).SortExpression = ClaimSystem.ClaimSystemDV.COL_MAINTAIN_CLAIM
                SortAndBindGrid()

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

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
            HiddenDeletePromptResponse.Value = ""
        End Sub

        Private Sub DoDelete()
            State.MyBO = New ClaimSystem(State.Id)

            Try
                State.MyBO.Delete()
                State.MyBO.Save()
            Catch ex As Exception
                State.MyBO.RejectChanges()
                Throw ex
            End Try

            State.PageIndex = grdView.PageIndex

            'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
            State.IsAfterSave = True
            State.searchDV = Nothing
            PopulateGrid()
            State.PageIndex = grdView.PageIndex
        End Sub

        Private Function GetDV() As DataView

            Dim dv As DataView

            State.searchDV = GetGridDataView()
            State.searchDV.Sort = grdView.DataMember()

            Return (State.searchDV)

        End Function

        Private Function GetGridDataView() As DataView

            With State
                Return (ClaimSystem.LoadList(.DescriptionMask))
            End With

        End Function

        Private Sub SetStateProperties()

            State.DescriptionMask = SearchDescriptionTextBox.Text

        End Sub

        Private Sub AddNew()

            Dim dv As DataView

            State.searchDV = GetGridDataView()

            State.MyBO = New ClaimSystem
            State.Id = State.MyBO.Id

            State.searchDV = State.MyBO.GetNewDataViewRow(State.searchDV, State.Id, State.MyBO)
            grdView.DataSource = State.searchDV

            SetPageAndSelectedIndexFromGuid(State.searchDV, State.Id, grdView, State.PageIndex, State.IsEditMode)

            grdView.DataBind()

            State.PageIndex = grdView.PageIndex

            SetGridControls(grdView, False)

            'Set focus on the BusinessUnit TextBox for the EditItemIndex row
            SetFocusOnEditableFieldInGrid(grdView, DESCRIPTION_COL, DESCRIPTION_CONTROL_NAME, grdView.EditIndex)

            AssignSelectedRecordFromBO()

            'Me.TranslateGridControls(Grid)
            SetButtonsState()
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, grdView)
        End Sub

        Private Sub SortAndBindGrid()

            State.PageIndex = grdView.PageIndex
            If (State.searchDV.Count = 0) Then

                State.bnoRow = True
                CreateHeaderForEmptyGrid(grdView, SortDirection)
            Else
                State.bnoRow = False
                grdView.Enabled = True
                grdView.DataSource = State.searchDV
                HighLightSortColumn(grdView, State.SortExpression)
                grdView.DataBind()
            End If

            If Not grdView.BottomPagerRow.Visible Then grdView.BottomPagerRow.Visible = True

            ControlMgr.SetVisibleControl(Me, grdView, State.IsGridVisible)

            ControlMgr.SetVisibleControl(Me, trPageSize, grdView.Visible)

            Session("recCount") = State.searchDV.Count

            If grdView.Visible Then
                If (State.AddingNewRow) Then
                    lblRecordCount.Text = (State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, grdView)
        End Sub

        Private Sub AssignBOFromSelectedRecord()

            Try
                With State.MyBO
                    .Code = CType(grdView.Rows(grdView.EditIndex).Cells(CODE_COL).FindControl(CODE_CONTROL_NAME), TextBox).Text
                    .Description = CType(grdView.Rows(grdView.EditIndex).Cells(DESCRIPTION_COL).FindControl(DESCRIPTION_CONTROL_NAME), TextBox).Text
                    .NewClaimId = GetSelectedItem(CType(grdView.Rows(grdView.EditIndex).Cells(NEW_CLAIM_COL).FindControl(NEW_CLAIM_CONTROL_NAME), DropDownList))
                    .PayClaimId = GetSelectedItem(CType(grdView.Rows(grdView.EditIndex).Cells(PAY_CLAIM_COL).FindControl(PAY_CLAIM_CONTROL_NAME), DropDownList))
                    .MaintainClaimId = GetSelectedItem(CType(grdView.Rows(grdView.EditIndex).Cells(MAINTAIN_CLAIM_COL).FindControl(MAINTAIN_CLAIM_CONTROL_NAME), DropDownList))
                End With
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub AssignSelectedRecordFromBO()

            Dim gridRowIdx As Integer = grdView.EditIndex
            Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

            Try
                With State.MyBO

                    CType(grdView.Rows(gridRowIdx).Cells(ID_COL).FindControl(ID_CONTROL_NAME), Label).Text = .Id.ToString

                    If .Description IsNot Nothing Then
                        CType(grdView.Rows(gridRowIdx).Cells(DESCRIPTION_COL).FindControl(DESCRIPTION_CONTROL_NAME), TextBox).Text = .Description
                    End If

                    If .Code IsNot Nothing Then
                        CType(grdView.Rows(gridRowIdx).Cells(CODE_COL).FindControl(CODE_CONTROL_NAME), TextBox).Text = .Code
                    End If

                    'BindListControlToDataView(CType(Me.grdView.Rows(gridRowIdx).Cells(Me.NEW_CLAIM_COL).FindControl(Me.NEW_CLAIM_CONTROL_NAME), DropDownList), LookupListNew.GetYesNoLookupList(oLanguageId), , , False)
                    Dim yesNoLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())
                    CType(grdView.Rows(gridRowIdx).Cells(NEW_CLAIM_COL).FindControl(NEW_CLAIM_CONTROL_NAME), DropDownList).Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = False
                                                       })
                    If Not .NewClaimId.Equals(Guid.Empty) Then
                        SetSelectedItem(CType(grdView.Rows(gridRowIdx).Cells(NEW_CLAIM_COL).FindControl(NEW_CLAIM_CONTROL_NAME), DropDownList), .NewClaimId)
                    End If

                    'BindListControlToDataView(CType(Me.grdView.Rows(gridRowIdx).Cells(Me.PAY_CLAIM_COL).FindControl(Me.PAY_CLAIM_CONTROL_NAME), DropDownList), LookupListNew.GetYesNoLookupList(oLanguageId), , , False)
                    CType(grdView.Rows(gridRowIdx).Cells(PAY_CLAIM_COL).FindControl(PAY_CLAIM_CONTROL_NAME), DropDownList).Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = False
                                                       })
                    If Not .PayClaimId.Equals(Guid.Empty) Then
                        SetSelectedItem(CType(grdView.Rows(gridRowIdx).Cells(PAY_CLAIM_COL).FindControl(PAY_CLAIM_CONTROL_NAME), DropDownList), .PayClaimId)
                    End If

                    'BindListControlToDataView(CType(Me.grdView.Rows(gridRowIdx).Cells(Me.MAINTAIN_CLAIM_COL).FindControl(Me.MAINTAIN_CLAIM_CONTROL_NAME), DropDownList), LookupListNew.GetYesNoLookupList(oLanguageId), , , False)
                    CType(grdView.Rows(gridRowIdx).Cells(MAINTAIN_CLAIM_COL).FindControl(MAINTAIN_CLAIM_CONTROL_NAME), DropDownList).Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = False
                                                       })
                    If Not .MaintainClaimId.Equals(Guid.Empty) Then
                        SetSelectedItem(CType(grdView.Rows(gridRowIdx).Cells(MAINTAIN_CLAIM_COL).FindControl(MAINTAIN_CLAIM_CONTROL_NAME), DropDownList), .MaintainClaimId)
                    End If

                End With
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub ReturnFromEditing()

            grdView.EditIndex = NO_ROW_SELECTED_INDEX

            If grdView.PageCount = 0 Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, grdView, False)
            Else
                ControlMgr.SetVisibleControl(Me, grdView, True)
            End If

            State.IsEditMode = False
            PopulateGrid()
            State.PageIndex = grdView.PageIndex
            SetButtonsState()

        End Sub

        Private Sub SetButtonsState()

            If (State.IsEditMode) Then
                ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, True)
                ControlMgr.SetVisibleControl(Me, CancelButton, True)
                ControlMgr.SetVisibleControl(Me, NewButton_WRITE, False)
                ControlMgr.SetEnableControl(Me, SearchButton, False)
                ControlMgr.SetEnableControl(Me, ClearButton, False)
                MenuEnabled = False
                If (cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, False)
                End If
            Else
                ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, False)
                ControlMgr.SetVisibleControl(Me, CancelButton, False)
                ControlMgr.SetVisibleControl(Me, NewButton_WRITE, True)
                ControlMgr.SetEnableControl(Me, SearchButton, True)
                ControlMgr.SetEnableControl(Me, ClearButton, True)
                MenuEnabled = True
                If (cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, True)
                End If
            End If

        End Sub

        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                grdView.PageIndex = NewCurrentPageIndex(grdView, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub


#End Region

#Region " Datagrid Related "

        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

        Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdView.PageIndexChanging

            Try
                If (Not (State.IsEditMode)) Then
                    State.PageIndex = e.NewPageIndex
                    grdView.PageIndex = State.PageIndex
                    PopulateGrid()
                    grdView.SelectedIndex = NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Protected Sub ItemCommand(source As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Try
                Dim index As Integer

                If (e.CommandName = EDIT_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    State.IsEditMode = True
                    State.Id = New Guid(CType(grdView.Rows(index).Cells(ID_COL).FindControl(ID_CONTROL_NAME), Label).Text)
                    State.MyBO = New ClaimSystem(State.Id)

                    PopulateGrid()

                    State.PageIndex = grdView.PageIndex

                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(grdView, False)

                    'Set focus on the Description TextBox for the EditItemIndex row
                    SetFocusOnEditableFieldInGrid(grdView, DESCRIPTION_COL, DESCRIPTION_CONTROL_NAME, index)

                    AssignSelectedRecordFromBO()

                    SetButtonsState()

                ElseIf (e.CommandName = DELETE_COMMAND) Then
                    index = CInt(e.CommandArgument)

                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    grdView.SelectedIndex = NO_ROW_SELECTED_INDEX

                    State.Id = New Guid(CType(grdView.Rows(index).Cells(ID_COL).FindControl(ID_CONTROL_NAME), Label).Text)
                    DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenDeletePromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
                End If

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        'The Binding Logic is here
        Private Sub ItemDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdView.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

            If dvRow IsNot Nothing And Not State.bnoRow Then

                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then

                    CType(e.Row.Cells(ID_COL).FindControl(ID_CONTROL_NAME), Label).Text = GetGuidStringFromByteArray(CType(dvRow(ClaimSystem.ClaimSystemDV.COL_CLAIM_SYSTEM_ID), Byte()))

                    If (State.IsEditMode = True _
                            AndAlso State.Id.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(ClaimSystem.ClaimSystemDV.COL_CLAIM_SYSTEM_ID), Byte())))) Then

                    Else

                        Dim dRow() As DataRow

                        CType(e.Row.Cells(DESCRIPTION_COL).FindControl(DESCRIPTION_LABEL_CONTROL_NAME), Label).Text = dvRow(ClaimSystem.ClaimSystemDV.COL_DESCRIPTION).ToString
                        CType(e.Row.Cells(CODE_COL).FindControl(CODE_LABEL_CONTROL_NAME), Label).Text = dvRow(ClaimSystem.ClaimSystemDV.COL_CODE).ToString

                        Dim newClaimLabel As String = dvRow(ClaimSystem.ClaimSystemDV.COL_NEW_CLAIM).ToString
                        dRow = State.YESNOdv.Table.Select("code='" & newClaimLabel & "' and language_id='" & GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.LanguageId) & "'")
                        If (dRow IsNot Nothing AndAlso dRow.Length > 0) Then
                            CType(e.Row.Cells(NEW_CLAIM_COL).FindControl(NEW_CLAIM_LABEL_CONTROL_NAME), Label).Text = CType(dRow(0).Item("Description"), String)
                        End If

                        Dim payClaimLabel As String = dvRow(ClaimSystem.ClaimSystemDV.COL_PAY_CLAIM).ToString
                        dRow = State.YESNOdv.Table.Select("code='" & payClaimLabel & "' and language_id='" & GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.LanguageId) & "'")
                        If (dRow IsNot Nothing AndAlso dRow.Length > 0) Then
                            CType(e.Row.Cells(PAY_CLAIM_COL).FindControl(PAY_CLAIM_LABEL_CONTROL_NAME), Label).Text = CType(dRow(0).Item("Description"), String)
                        End If

                        Dim maintainClaimLabel As String = dvRow(ClaimSystem.ClaimSystemDV.COL_MAINTAIN_CLAIM).ToString
                        dRow = State.YESNOdv.Table.Select("code='" & maintainClaimLabel & "' and language_id='" & GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.LanguageId) & "'")
                        If (dRow IsNot Nothing AndAlso dRow.Length > 0) Then
                            CType(e.Row.Cells(MAINTAIN_CLAIM_COL).FindControl(MAINTAIN_CLAIM_LABEL_CONTROL_NAME), Label).Text = CType(dRow(0).Item("Description"), String)
                        End If

                    End If
                End If
            End If
        End Sub

        Protected Sub ItemBound(source As Object, e As GridViewRowEventArgs) Handles grdView.RowDataBound

            Try
                If Not State.bnoRow Then
                    BaseItemBound(source, e)
                End If

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Protected Sub ItemCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdView.Sorting
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

                State.Id = Guid.Empty
                State.PageIndex = 0

                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()
            BindBOPropertyToGridHeader(State.MyBO, "Description", grdView.Columns(DESCRIPTION_COL))
            BindBOPropertyToGridHeader(State.MyBO, "Code", grdView.Columns(CODE_COL))
            BindBOPropertyToGridHeader(State.MyBO, "NewClaimId", grdView.Columns(NEW_CLAIM_COL))
            BindBOPropertyToGridHeader(State.MyBO, "PayClaimId", grdView.Columns(PAY_CLAIM_COL))
            BindBOPropertyToGridHeader(State.MyBO, "MaintainClaimId", grdView.Columns(MAINTAIN_CLAIM_COL))
            ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(grid As GridView, cellPosition As Integer, controlName As String, itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim ctl As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(ctl)
        End Sub

#End Region

    End Class

End Namespace
