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
                IsEditing = (Me.grdView.EditIndex > NO_ROW_SELECTED_INDEX)
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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

        Private Sub SearchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchButton.Click
            Try
                Me.State.IsGridVisible = True
                Me.State.searchDV = Nothing
                PopulateGrid()
                'Me.State.PageIndex = Me.grdView.CurrentPageIndex
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub ClearButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearButton.Click

            Try
                ClearSearchCriteria()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub ClearSearchCriteria()

            Try
                SearchDescriptionTextBox.Text = String.Empty

                'Update Page State
                With Me.State
                    .DescriptionMask = SearchDescriptionTextBox.Text
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub NewButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewButton_WRITE.Click

            Try
                Me.State.IsEditMode = True
                Me.State.IsGridVisible = True
                Me.State.AddingNewRow = True
                AddNew()
                SetButtonsState()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub SaveButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton_WRITE.Click

            Try

                AssignBOFromSelectedRecord()
                If (Me.State.MyBO.IsDirty) Then
                    Me.State.MyBO.Save()
                    Me.State.IsAfterSave = True
                    Me.State.AddingNewRow = False
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

        Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click

            Try
                Me.grdView.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                Me.State.Canceling = True
                If (Me.State.AddingNewRow) Then
                    Me.State.AddingNewRow = False
                    Me.State.searchDV = Nothing
                End If
                ReturnFromEditing()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

#End Region


#Region "Private Methods"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            'Put user code to initialize the page here
            Try
                Me.ErrControllerMaster.Clear_Hide()

                Me.SetFormTitle(PAGETITLE)
                Me.SetFormTab(PAGETAB)

                Me.SetStateProperties()
                If Not Page.IsPostBack Then
                    Me.SortDirection = ClaimSystem.ClaimSystemDV.COL_DESCRIPTION
                    Me.SetDefaultButton(Me.SearchDescriptionTextBox, Me.SearchButton)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Me.SetGridItemStyleColor(grdView)
                    Me.State.PageIndex = 0
                    If Me.State.MyBO Is Nothing Then
                        Me.State.MyBO = New ClaimSystem
                    End If
                    SetButtonsState()
                    Me.TranslateGridHeader(Me.grdView)
                    Me.TranslateGridControls(Me.grdView)
                    PopulateAll()
                Else
                    CheckIfComingFromDeleteConfirm()
                End If
                BindBoPropertiesToGridHeaders()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
            Me.ShowMissingTranslations(ErrControllerMaster)
        End Sub

        Private Sub PopulateAll()
            Dim YESNOdv As DataView = LookupListNew.DropdownLookupList(YESNO, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
            Me.State.YESNOdv = YESNOdv

        End Sub

        Private Sub PopulateGrid()

            Dim dv As DataView

            Try

                If (Me.State.searchDV Is Nothing) Then
                    Me.State.searchDV = GetDV()
                End If
                Me.State.searchDV.Sort = Me.State.SortExpression

                If (Me.State.IsAfterSave) Then
                    Me.State.IsAfterSave = False
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.grdView, Me.State.PageIndex)
                ElseIf (Me.State.IsEditMode) Then
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.grdView, Me.State.PageIndex, Me.State.IsEditMode)
                Else
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.grdView, Me.State.PageIndex)
                End If

                Me.grdView.AutoGenerateColumns = False
                Me.grdView.Columns(Me.CODE_COL).SortExpression = ClaimSystem.ClaimSystemDV.COL_CODE
                Me.grdView.Columns(Me.DESCRIPTION_COL).SortExpression = ClaimSystem.ClaimSystemDV.COL_DESCRIPTION
                Me.grdView.Columns(Me.NEW_CLAIM_COL).SortExpression = ClaimSystem.ClaimSystemDV.COL_NEW_CLAIM
                Me.grdView.Columns(Me.PAY_CLAIM_COL).SortExpression = ClaimSystem.ClaimSystemDV.COL_PAY_CLAIM
                Me.grdView.Columns(Me.MAINTAIN_CLAIM_COL).SortExpression = ClaimSystem.ClaimSystemDV.COL_MAINTAIN_CLAIM
                Me.SortAndBindGrid()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

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
            Me.HiddenDeletePromptResponse.Value = ""
        End Sub

        Private Sub DoDelete()
            Me.State.MyBO = New ClaimSystem(Me.State.Id)

            Try
                Me.State.MyBO.Delete()
                Me.State.MyBO.Save()
            Catch ex As Exception
                Me.State.MyBO.RejectChanges()
                Throw ex
            End Try

            Me.State.PageIndex = grdView.PageIndex

            'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
            Me.State.IsAfterSave = True
            Me.State.searchDV = Nothing
            PopulateGrid()
            Me.State.PageIndex = grdView.PageIndex
        End Sub

        Private Function GetDV() As DataView

            Dim dv As DataView

            Me.State.searchDV = GetGridDataView()
            Me.State.searchDV.Sort = Me.grdView.DataMember()

            Return (Me.State.searchDV)

        End Function

        Private Function GetGridDataView() As DataView

            With State
                Return (ClaimSystem.LoadList(.DescriptionMask))
            End With

        End Function

        Private Sub SetStateProperties()

            Me.State.DescriptionMask = SearchDescriptionTextBox.Text

        End Sub

        Private Sub AddNew()

            Dim dv As DataView

            Me.State.searchDV = GetGridDataView()

            Me.State.MyBO = New ClaimSystem
            Me.State.Id = Me.State.MyBO.Id

            Me.State.searchDV = Me.State.MyBO.GetNewDataViewRow(Me.State.searchDV, Me.State.Id, Me.State.MyBO)
            grdView.DataSource = Me.State.searchDV

            Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.grdView, Me.State.PageIndex, Me.State.IsEditMode)

            grdView.DataBind()

            Me.State.PageIndex = grdView.PageIndex

            SetGridControls(Me.grdView, False)

            'Set focus on the BusinessUnit TextBox for the EditItemIndex row
            Me.SetFocusOnEditableFieldInGrid(Me.grdView, Me.DESCRIPTION_COL, Me.DESCRIPTION_CONTROL_NAME, Me.grdView.EditIndex)

            Me.AssignSelectedRecordFromBO()

            'Me.TranslateGridControls(Grid)
            Me.SetButtonsState()
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, grdView)
        End Sub

        Private Sub SortAndBindGrid()

            Me.State.PageIndex = Me.grdView.PageIndex
            If (Me.State.searchDV.Count = 0) Then

                Me.State.bnoRow = True
                CreateHeaderForEmptyGrid(grdView, Me.SortDirection)
            Else
                Me.State.bnoRow = False
                Me.grdView.Enabled = True
                Me.grdView.DataSource = Me.State.searchDV
                HighLightSortColumn(grdView, Me.State.SortExpression)
                Me.grdView.DataBind()
            End If

            If Not grdView.BottomPagerRow.Visible Then grdView.BottomPagerRow.Visible = True

            ControlMgr.SetVisibleControl(Me, grdView, Me.State.IsGridVisible)

            ControlMgr.SetVisibleControl(Me, trPageSize, Me.grdView.Visible)

            Session("recCount") = Me.State.searchDV.Count

            If Me.grdView.Visible Then
                If (Me.State.AddingNewRow) Then
                    Me.lblRecordCount.Text = (Me.State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, grdView)
        End Sub

        Private Sub AssignBOFromSelectedRecord()

            Try
                With Me.State.MyBO
                    .Code = CType(Me.grdView.Rows(Me.grdView.EditIndex).Cells(Me.CODE_COL).FindControl(Me.CODE_CONTROL_NAME), TextBox).Text
                    .Description = CType(Me.grdView.Rows(Me.grdView.EditIndex).Cells(Me.DESCRIPTION_COL).FindControl(Me.DESCRIPTION_CONTROL_NAME), TextBox).Text
                    .NewClaimId = Me.GetSelectedItem(CType(Me.grdView.Rows(Me.grdView.EditIndex).Cells(Me.NEW_CLAIM_COL).FindControl(Me.NEW_CLAIM_CONTROL_NAME), DropDownList))
                    .PayClaimId = Me.GetSelectedItem(CType(Me.grdView.Rows(Me.grdView.EditIndex).Cells(Me.PAY_CLAIM_COL).FindControl(Me.PAY_CLAIM_CONTROL_NAME), DropDownList))
                    .MaintainClaimId = Me.GetSelectedItem(CType(Me.grdView.Rows(Me.grdView.EditIndex).Cells(Me.MAINTAIN_CLAIM_COL).FindControl(Me.MAINTAIN_CLAIM_CONTROL_NAME), DropDownList))
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub AssignSelectedRecordFromBO()

            Dim gridRowIdx As Integer = Me.grdView.EditIndex
            Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

            Try
                With Me.State.MyBO

                    CType(Me.grdView.Rows(gridRowIdx).Cells(Me.ID_COL).FindControl(Me.ID_CONTROL_NAME), Label).Text = .Id.ToString

                    If Not .Description Is Nothing Then
                        CType(Me.grdView.Rows(gridRowIdx).Cells(Me.DESCRIPTION_COL).FindControl(Me.DESCRIPTION_CONTROL_NAME), TextBox).Text = .Description
                    End If

                    If Not .Code Is Nothing Then
                        CType(Me.grdView.Rows(gridRowIdx).Cells(Me.CODE_COL).FindControl(Me.CODE_CONTROL_NAME), TextBox).Text = .Code
                    End If

                    'BindListControlToDataView(CType(Me.grdView.Rows(gridRowIdx).Cells(Me.NEW_CLAIM_COL).FindControl(Me.NEW_CLAIM_CONTROL_NAME), DropDownList), LookupListNew.GetYesNoLookupList(oLanguageId), , , False)
                    Dim yesNoLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())
                    CType(Me.grdView.Rows(gridRowIdx).Cells(Me.NEW_CLAIM_COL).FindControl(Me.NEW_CLAIM_CONTROL_NAME), DropDownList).Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = False
                                                       })
                    If Not .NewClaimId.Equals(Guid.Empty) Then
                        Me.SetSelectedItem(CType(Me.grdView.Rows(gridRowIdx).Cells(Me.NEW_CLAIM_COL).FindControl(Me.NEW_CLAIM_CONTROL_NAME), DropDownList), .NewClaimId)
                    End If

                    'BindListControlToDataView(CType(Me.grdView.Rows(gridRowIdx).Cells(Me.PAY_CLAIM_COL).FindControl(Me.PAY_CLAIM_CONTROL_NAME), DropDownList), LookupListNew.GetYesNoLookupList(oLanguageId), , , False)
                    CType(Me.grdView.Rows(gridRowIdx).Cells(Me.PAY_CLAIM_COL).FindControl(Me.PAY_CLAIM_CONTROL_NAME), DropDownList).Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = False
                                                       })
                    If Not .PayClaimId.Equals(Guid.Empty) Then
                        Me.SetSelectedItem(CType(Me.grdView.Rows(gridRowIdx).Cells(Me.PAY_CLAIM_COL).FindControl(Me.PAY_CLAIM_CONTROL_NAME), DropDownList), .PayClaimId)
                    End If

                    'BindListControlToDataView(CType(Me.grdView.Rows(gridRowIdx).Cells(Me.MAINTAIN_CLAIM_COL).FindControl(Me.MAINTAIN_CLAIM_CONTROL_NAME), DropDownList), LookupListNew.GetYesNoLookupList(oLanguageId), , , False)
                    CType(Me.grdView.Rows(gridRowIdx).Cells(Me.MAINTAIN_CLAIM_COL).FindControl(Me.MAINTAIN_CLAIM_CONTROL_NAME), DropDownList).Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = False
                                                       })
                    If Not .MaintainClaimId.Equals(Guid.Empty) Then
                        Me.SetSelectedItem(CType(Me.grdView.Rows(gridRowIdx).Cells(Me.MAINTAIN_CLAIM_COL).FindControl(Me.MAINTAIN_CLAIM_CONTROL_NAME), DropDownList), .MaintainClaimId)
                    End If

                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub ReturnFromEditing()

            grdView.EditIndex = NO_ROW_SELECTED_INDEX

            If Me.grdView.PageCount = 0 Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, grdView, False)
            Else
                ControlMgr.SetVisibleControl(Me, grdView, True)
            End If

            Me.State.IsEditMode = False
            Me.PopulateGrid()
            Me.State.PageIndex = grdView.PageIndex
            SetButtonsState()

        End Sub

        Private Sub SetButtonsState()

            If (Me.State.IsEditMode) Then
                ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, True)
                ControlMgr.SetVisibleControl(Me, CancelButton, True)
                ControlMgr.SetVisibleControl(Me, NewButton_WRITE, False)
                ControlMgr.SetEnableControl(Me, SearchButton, False)
                ControlMgr.SetEnableControl(Me, ClearButton, False)
                Me.MenuEnabled = False
                If (Me.cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, False)
                End If
            Else
                ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, False)
                ControlMgr.SetVisibleControl(Me, CancelButton, False)
                ControlMgr.SetVisibleControl(Me, NewButton_WRITE, True)
                ControlMgr.SetEnableControl(Me, SearchButton, True)
                ControlMgr.SetEnableControl(Me, ClearButton, True)
                Me.MenuEnabled = True
                If (Me.cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, True)
                End If
            End If

        End Sub

        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                grdView.PageIndex = NewCurrentPageIndex(grdView, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub


#End Region

#Region " Datagrid Related "

        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(ByVal value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

        Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdView.PageIndexChanging

            Try
                If (Not (Me.State.IsEditMode)) Then
                    Me.State.PageIndex = e.NewPageIndex
                    Me.grdView.PageIndex = Me.State.PageIndex
                    Me.PopulateGrid()
                    Me.grdView.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Protected Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Try
                Dim index As Integer

                If (e.CommandName = Me.EDIT_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    Me.State.IsEditMode = True
                    Me.State.Id = New Guid(CType(Me.grdView.Rows(index).Cells(Me.ID_COL).FindControl(Me.ID_CONTROL_NAME), Label).Text)
                    Me.State.MyBO = New ClaimSystem(Me.State.Id)

                    Me.PopulateGrid()

                    Me.State.PageIndex = grdView.PageIndex

                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(Me.grdView, False)

                    'Set focus on the Description TextBox for the EditItemIndex row
                    Me.SetFocusOnEditableFieldInGrid(Me.grdView, Me.DESCRIPTION_COL, Me.DESCRIPTION_CONTROL_NAME, index)

                    Me.AssignSelectedRecordFromBO()

                    Me.SetButtonsState()

                ElseIf (e.CommandName = Me.DELETE_COMMAND) Then
                    index = CInt(e.CommandArgument)

                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    grdView.SelectedIndex = Me.NO_ROW_SELECTED_INDEX

                    Me.State.Id = New Guid(CType(Me.grdView.Rows(index).Cells(Me.ID_COL).FindControl(Me.ID_CONTROL_NAME), Label).Text)
                    Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenDeletePromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        'The Binding Logic is here
        Private Sub ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdView.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

            If Not dvRow Is Nothing And Not Me.State.bnoRow Then

                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then

                    CType(e.Row.Cells(Me.ID_COL).FindControl(Me.ID_CONTROL_NAME), Label).Text = GetGuidStringFromByteArray(CType(dvRow(ClaimSystem.ClaimSystemDV.COL_CLAIM_SYSTEM_ID), Byte()))

                    If (Me.State.IsEditMode = True _
                            AndAlso Me.State.Id.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(ClaimSystem.ClaimSystemDV.COL_CLAIM_SYSTEM_ID), Byte())))) Then

                    Else

                        Dim dRow() As DataRow

                        CType(e.Row.Cells(Me.DESCRIPTION_COL).FindControl(Me.DESCRIPTION_LABEL_CONTROL_NAME), Label).Text = dvRow(ClaimSystem.ClaimSystemDV.COL_DESCRIPTION).ToString
                        CType(e.Row.Cells(Me.CODE_COL).FindControl(Me.CODE_LABEL_CONTROL_NAME), Label).Text = dvRow(ClaimSystem.ClaimSystemDV.COL_CODE).ToString

                        Dim newClaimLabel As String = dvRow(ClaimSystem.ClaimSystemDV.COL_NEW_CLAIM).ToString
                        dRow = Me.State.YESNOdv.Table.Select("code='" & newClaimLabel & "' and language_id='" & GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.LanguageId) & "'")
                        If (Not dRow Is Nothing AndAlso dRow.Length > 0) Then
                            CType(e.Row.Cells(Me.NEW_CLAIM_COL).FindControl(Me.NEW_CLAIM_LABEL_CONTROL_NAME), Label).Text = CType(dRow(0).Item("Description"), String)
                        End If

                        Dim payClaimLabel As String = dvRow(ClaimSystem.ClaimSystemDV.COL_PAY_CLAIM).ToString
                        dRow = Me.State.YESNOdv.Table.Select("code='" & payClaimLabel & "' and language_id='" & GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.LanguageId) & "'")
                        If (Not dRow Is Nothing AndAlso dRow.Length > 0) Then
                            CType(e.Row.Cells(Me.PAY_CLAIM_COL).FindControl(Me.PAY_CLAIM_LABEL_CONTROL_NAME), Label).Text = CType(dRow(0).Item("Description"), String)
                        End If

                        Dim maintainClaimLabel As String = dvRow(ClaimSystem.ClaimSystemDV.COL_MAINTAIN_CLAIM).ToString
                        dRow = Me.State.YESNOdv.Table.Select("code='" & maintainClaimLabel & "' and language_id='" & GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.LanguageId) & "'")
                        If (Not dRow Is Nothing AndAlso dRow.Length > 0) Then
                            CType(e.Row.Cells(Me.MAINTAIN_CLAIM_COL).FindControl(Me.MAINTAIN_CLAIM_LABEL_CONTROL_NAME), Label).Text = CType(dRow(0).Item("Description"), String)
                        End If

                    End If
                End If
            End If
        End Sub

        Protected Sub ItemBound(ByVal source As Object, ByVal e As GridViewRowEventArgs) Handles grdView.RowDataBound

            Try
                If Not Me.State.bnoRow Then
                    BaseItemBound(source, e)
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Protected Sub ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdView.Sorting
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

                Me.State.Id = Guid.Empty
                Me.State.PageIndex = 0

                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "Description", Me.grdView.Columns(Me.DESCRIPTION_COL))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "Code", Me.grdView.Columns(Me.CODE_COL))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "NewClaimId", Me.grdView.Columns(Me.NEW_CLAIM_COL))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "PayClaimId", Me.grdView.Columns(Me.PAY_CLAIM_COL))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "MaintainClaimId", Me.grdView.Columns(Me.MAINTAIN_CLAIM_COL))
            Me.ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As GridView, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim ctl As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(ctl)
        End Sub

#End Region

    End Class

End Namespace
