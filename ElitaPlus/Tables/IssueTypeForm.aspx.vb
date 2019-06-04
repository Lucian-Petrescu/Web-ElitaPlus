Option Strict On
Option Explicit On

Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Tables

    Partial Class IssueTypeForm
        Inherits ElitaPlusSearchPage

        Private Class PageStatus
            Public Sub New()
                _pageIndex = 0
                _pageCount = 0
            End Sub
        End Class

#Region "Member Variables"

        Private Shared _pageIndex As Integer
        Private Shared _pageCount As Integer

#End Region

#Region "Properties"
        Public ReadOnly Property IsEditing() As Boolean
            Get
                IsEditing = (Me.Grid.EditItemIndex > NO_ROW_SELECTED_INDEX)
            End Get
        End Property
#End Region

#Region "Page State"

        Class MyState
            Public PageIndex As Integer = 0
            Public MyBO As IssueType

            Public DescriptionMask As String
            Public CodeMask As String
            Public Id As Guid

            Public IsAfterSave As Boolean
            Public IsEditMode As Boolean
            Public IsGridVisible As Boolean
            Public HasDataChanged As Boolean
            Public SearchDV As DataView = Nothing
            Public SelectedPageSize As Integer = DEFAULT_PAGE_SIZE
            Public AddingNewRow As Boolean
            Public Canceling As Boolean

            Public YesNoDV As DataView = Nothing
            Public SelectedIssueTypeId As Guid = Guid.Empty
            Public Code As String = String.Empty
            Public Description As String = String.Empty
            Public IsSystemGenerted As String = String.Empty
            Public IsSelfClearing As String = String.Empty
            Public SortExpression As String = IssueType.IssueTypeSearchDV.COL_NAME_CODE
            Public SearchClick As Boolean = False

            Public IssueTypeCode As String = "ISSTYP"

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

        'Protected WithEvents ErrController As ErrorController

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private _designerPlaceholderDeclaration As System.Object

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Constants"

        Private Const GRID_COL_IDX As Integer = 2
        Private Const GRID_COL_CODE_IDX As Integer = 3
        Private Const GRID_COL_DESCRIPTION_IDX As Integer = 4
        Private Const GRID_COL_IS_SYSTEM_GENERATED_IDX As Integer = 5
        Private Const GRID_COL_IS_SELF_CLEANING_IDX As Integer = 6

        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

        'Text Boxes
        Private Const DESCRIPTION_CONTROL_NAME As String = "DescriptionTextBox"
        Private Const CODE_CONTROL_NAME As String = "CodeTextBox"

        'Controls
        Private Const IS_SELF_CLEAN_CONTROL_NAME As String = "IsSelfCleaningDropDown"
        Private Const IS_SYS_GEN_CONTROL_NAME As String = "IsSystemGenDropdown"

        Private Const EDIT_COMMAND As String = "EditRecord"
        Private Const DELETE_COMMAND As String = "DeleteRecord"

        Private Const YESNO As String = "YESNO"

        Private Const NO_ROW_SELECTED_INDEX As Integer = -1

        Public Const PAGETITLE As String = "ISSUE_TYPE"
        Public Const PAGETAB As String = "Admin"

        Public Const IssueTypeCode As String = "ISSTYP"

#End Region

#Region "Button Click Handlers"

        Private Sub SearchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchButton.Click
            Try
                Me.State.Id = Guid.Empty
                Me.State.PageIndex = 0
                Me.State.IsGridVisible = True
                Me.State.SearchDV = Nothing
                Me.State.HasDataChanged = False
                Me.State.SearchClick = True
                Me.PopulateGrid()

                Me.State.PageIndex = Grid.CurrentPageIndex
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub ClearButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearButton.Click
            ClearSearchCriteria()
        End Sub

        Private Sub ClearSearchCriteria()
            Try
                moDescriptionText.Text = String.Empty
                moCodeText.Text = String.Empty
                ReturnFromEditing()

                'Update Page State
                With Me.State
                    .DescriptionMask = moDescriptionText.Text
                    .CodeMask = moCodeText.Text
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
                AddNewBlankRow()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub


        Private Sub SaveButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton_WRITE.Click
            Try
                PopulateBOFromForm()
                If (Me.State.MyBO.IsDirty) Then
                    Me.State.MyBO.Save()
                    Me.State.IsAfterSave = True
                    Me.State.AddingNewRow = False
                    Me.AddInfoMsg(MSG_RECORD_SAVED_OK)
                    Me.State.SearchDV = Nothing
                    Me.ReturnFromEditing()
                Else
                    Me.AddInfoMsg(MSG_RECORD_NOT_SAVED)
                    Me.ReturnFromEditing()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
            Me.PopulateGrid()
            SetGridControls(Me.Grid, False)

        End Sub


        Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click
            Try
                Me.Grid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                Me.State.Canceling = True
                If (Me.State.AddingNewRow) Then
                    Me.State.AddingNewRow = False
                    Me.State.SearchDV = Nothing
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
                ErrControllerMaster.Clear_Hide()

                Me.SetFormTitle(PAGETITLE)
                Me.SetFormTab(PAGETAB)

                Me.SetStateProperties()
                If Not Page.IsPostBack Then

                    SetDefaultButton(Me.moCodeText, Me.SearchButton)
                    SetDefaultButton(Me.moDescriptionText, Me.SearchButton)

                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Me.SetGridItemStyleColor(Me.Grid)
                    Me.State.PageIndex = 0
                    If Me.State.MyBO Is Nothing Then
                        Me.State.MyBO = New IssueType
                    End If
                    SetButtonsState()

                    Me.TranslateGridControls(Me.Grid)
                    PopulateYesNo()
                End If
                BindBoPropertiesToGridHeaders()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
            Me.ShowMissingTranslations(ErrControllerMaster)

        End Sub

        Private Sub PopulateYesNo()
            Dim yesNoDV As DataView = LookupListNew.DropdownLookupList(YESNO, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
            Me.State.YesNoDV = yesNoDV
        End Sub

        Private Sub PopulateGrid()
            Try
                If ((Me.State.SearchDV Is Nothing) OrElse (Me.State.HasDataChanged)) Then
                    Me.State.Code = moCodeText.Text
                    Me.State.Description = moDescriptionText.Text
                    Me.State.SearchDV = IssueType.GetList(Me.State.Code, Me.State.Description)
                End If

                Me.State.SearchDV.Sort = Me.State.SortExpression

                If (Me.State.IsAfterSave) Then
                    Me.State.IsAfterSave = False
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.SearchDV, Me.State.Id, Me.Grid, Me.State.PageIndex)
                ElseIf (Me.State.IsEditMode) Then
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.SearchDV, Me.State.Id, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
                Else
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.SearchDV, Me.State.Id, Me.Grid, Me.State.PageIndex)
                End If

                Me.Grid.AutoGenerateColumns = False
                Me.Grid.Columns(GRID_COL_CODE_IDX).SortExpression = IssueType.IssueTypeSearchDV.COL_NAME_CODE
                Me.Grid.Columns(GRID_COL_DESCRIPTION_IDX).SortExpression = IssueType.IssueTypeSearchDV.COL_NAME_DESCRIPTION
                Me.Grid.Columns(GRID_COL_IS_SYSTEM_GENERATED_IDX).SortExpression = IssueType.IssueTypeSearchDV.COL_NAME_IS_SYSTEM_GENERATED_DESC
                Me.Grid.Columns(GRID_COL_IS_SELF_CLEANING_IDX).SortExpression = IssueType.IssueTypeSearchDV.COL_NAME_IS_SELF_CLEANING_DESC

                If Me.State.SearchClick Then
                    Me.ValidSearchResultCount(Me.State.SearchDV.Count, True)
                    Me.State.SearchClick = False
                End If
                Me.SortAndBindGrid()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Function GetGridDataView() As DataView
            With State
                Return IssueType.GetList(Me.State.Code, Me.State.Description)
            End With

        End Function

        Private Sub SetStateProperties()
            Me.State.DescriptionMask = moDescriptionText.Text
            Me.State.CodeMask = moCodeText.Text
        End Sub

        Private Sub SortAndBindGrid()
            Me.State.PageIndex = Me.Grid.CurrentPageIndex
            Me.Grid.DataSource = Me.State.SearchDV
            HighLightSortColumn(Grid, Me.State.SortExpression)
            Me.Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)
            Session("recCount") = Me.State.SearchDV.Count
            If Me.Grid.Visible Then
                If (Me.State.AddingNewRow) Then
                    Me.lblRecordCount.Text = (Me.State.SearchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    Me.lblRecordCount.Text = Me.State.SearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        End Sub


        Private Sub AddNewBlankRow()
            Dim dv As DataView

            Me.State.SearchDV = GetGridDataView()
            Me.State.MyBO = New IssueType
            Me.State.Id = Me.State.MyBO.Id
            Me.State.SearchDV = IssueType.GetNewDataViewRow(Me.State.SearchDV, Me.State.Id, Me.State.MyBO)

            'populate dropdown values
            Me.State.MyBO.MyDropDownParentCode = IssueTypeCode

            Grid.DataSource = Me.State.SearchDV
            SetGridControls(Me.Grid, False)

            Me.Grid.AutoGenerateColumns = False
            Me.SetPageAndSelectedIndexFromGuid(Me.State.SearchDV, Me.State.Id, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)

            Grid.DataBind()
            Me.State.PageIndex = Grid.CurrentPageIndex

            'Set focus on the BusinessUnit TextBox for the EditItemIndex row
            Me.SetFocusOnEditableFieldInGrid(Me.Grid, GRID_COL_DESCRIPTION_IDX, DESCRIPTION_CONTROL_NAME, Me.Grid.EditItemIndex)

            Me.SetButtonsState()
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        End Sub

        Private Sub PopulateBOFromForm()
            Try
                With Me.State.MyBO
                    'Populate bo properties for the dropdown
                    .MyDropDownParentCode = IssueTypeCode
                    .MyDropDownNewItemCode = CType(Me.Grid.Items(Me.Grid.EditItemIndex).Cells(GRID_COL_CODE_IDX).FindControl(CODE_CONTROL_NAME), TextBox).Text
                    .MyDropDownNewItemDesc = CType(Me.Grid.Items(Me.Grid.EditItemIndex).Cells(GRID_COL_DESCRIPTION_IDX).FindControl(DESCRIPTION_CONTROL_NAME), TextBox).Text

                    .Description = CType(Me.Grid.Items(Me.Grid.EditItemIndex).Cells(GRID_COL_DESCRIPTION_IDX).FindControl(DESCRIPTION_CONTROL_NAME), TextBox).Text
                    .Code = CType(Me.Grid.Items(Me.Grid.EditItemIndex).Cells(GRID_COL_CODE_IDX).FindControl(CODE_CONTROL_NAME), TextBox).Text
                    .IsSystemGenerated = GetSelectedItem(CType(Grid.Items(Grid.EditItemIndex).Cells(GRID_COL_IS_SYSTEM_GENERATED_IDX).FindControl(IS_SYS_GEN_CONTROL_NAME), DropDownList))
                    .IsSelfCleaning = GetSelectedItem(CType(Grid.Items(Grid.EditItemIndex).Cells(GRID_COL_CODE_IDX).FindControl(IS_SELF_CLEAN_CONTROL_NAME), DropDownList))
                End With

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub ReturnFromEditing()

            Grid.EditItemIndex = NO_ROW_SELECTED_INDEX
            If Me.Grid.PageCount = 0 Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, Grid, False)
            Else
                ControlMgr.SetVisibleControl(Me, Grid, True)
            End If
            SetGridControls(Grid, True)
            Me.State.IsEditMode = False
            Me.PopulateGrid()
            Me.State.PageIndex = Grid.CurrentPageIndex
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
                    ControlMgr.SetEnableControl(Me, Me.cboPageSize, True)
                End If
            End If

        End Sub

#End Region

#Region " Datagrid Related "

        'The Binding Logic is here
        Private Sub GridItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
            Dim itemType As ListItemType = e.Item.ItemType
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

            If (itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem) Then
                Dim dRow() As DataRow
                e.Item.Cells(GRID_COL_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(IssueType.IssueTypeSearchDV.COL_NAME_ISSUE_TYPE_ID), Byte()))
                e.Item.Cells(GRID_COL_CODE_IDX).Text = dvRow(IssueType.IssueTypeSearchDV.COL_NAME_CODE).ToString
                e.Item.Cells(GRID_COL_DESCRIPTION_IDX).Text = dvRow(IssueType.IssueTypeSearchDV.COL_NAME_DESCRIPTION).ToString

                dRow = Me.State.YesNoDV.Table.Select("code='" & dvRow(IssueType.IssueTypeSearchDV.COL_NAME_IS_SYSTEM_GENERATED_DESC).ToString & "' and language_id='" & GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.LanguageId) & "'")
                If (Not dRow Is Nothing AndAlso dRow.Length > 0) Then
                    e.Item.Cells(GRID_COL_IS_SYSTEM_GENERATED_IDX).Text = CType(dRow(0).Item("Description"), String).ToString
                End If
                dRow = Me.State.YesNoDV.Table.Select("code='" & dvRow(IssueType.IssueTypeSearchDV.COL_NAME_IS_SELF_CLEANING_DESC).ToString & "' and language_id='" & GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.LanguageId) & "'")
                If (Not dRow Is Nothing AndAlso dRow.Length > 0) Then
                    e.Item.Cells(GRID_COL_IS_SELF_CLEANING_IDX).Text = CType(dRow(0).Item("Description"), String).ToString
                End If

            ElseIf (itemType = ListItemType.EditItem) Then

                e.Item.Cells(GRID_COL_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(IssueType.IssueTypeSearchDV.COL_NAME_ISSUE_TYPE_ID), Byte()))
                CType(e.Item.Cells(GRID_COL_DESCRIPTION_IDX).FindControl(DESCRIPTION_CONTROL_NAME), TextBox).Text = Me.State.MyBO.Description
                CType(e.Item.Cells(GRID_COL_CODE_IDX).FindControl(CODE_CONTROL_NAME), TextBox).Text = Me.State.MyBO.Code

                ' BindListControlToDataView(CType(e.Item.Cells(GRID_COL_IS_SYSTEM_GENERATED_IDX).FindControl(IS_SYS_GEN_CONTROL_NAME), DropDownList), LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , False)
                Dim yesNoLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())
                CType(e.Item.Cells(GRID_COL_IS_SYSTEM_GENERATED_IDX).FindControl(IS_SYS_GEN_CONTROL_NAME), DropDownList).Populate(yesNoLkl, New PopulateOptions())
                If Not Me.State.MyBO.IsSystemGenerated.Equals(Guid.Empty) Then
                    SetSelectedItem(CType(e.Item.Cells(GRID_COL_IS_SYSTEM_GENERATED_IDX).FindControl(IS_SYS_GEN_CONTROL_NAME), DropDownList), Me.State.MyBO.IsSystemGenerated())
                End If

                'BindListControlToDataView(CType(e.Item.Cells(GRID_COL_IS_SELF_CLEANING_IDX).FindControl(IS_SELF_CLEAN_CONTROL_NAME), DropDownList), LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , False)
                CType(e.Item.Cells(GRID_COL_IS_SELF_CLEANING_IDX).FindControl(IS_SELF_CLEAN_CONTROL_NAME), DropDownList).Populate(yesNoLkl, New PopulateOptions())
                If Not Me.State.MyBO.IsSelfCleaning.Equals(Guid.Empty) Then
                    SetSelectedItem(CType(e.Item.Cells(GRID_COL_IS_SELF_CLEANING_IDX).FindControl(IS_SELF_CLEAN_CONTROL_NAME), DropDownList), Me.State.MyBO.IsSelfCleaning())
                End If
            End If

        End Sub

        Protected Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)

            Try

                If (e.CommandName = EDIT_COMMAND) Then
                    'Set the IsEditMode flag to TRUE
                    Me.State.IsEditMode = True

                    Me.State.Id = New Guid(Me.Grid.Items(e.Item.ItemIndex).Cells(GRID_COL_IDX).Text)
                    Me.State.MyBO = New IssueType(Me.State.Id)
                    Me.PopulateGrid()
                    Me.State.PageIndex = Grid.CurrentPageIndex

                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(Me.Grid, False)

                    'Set focus on the Description TextBox for the EditItemIndex row
                    Me.SetFocusOnEditableFieldInGrid(Me.Grid, GRID_COL_DESCRIPTION_IDX, DESCRIPTION_CONTROL_NAME, e.Item.ItemIndex)
                    Me.SetButtonsState()

                    'populate dropdown values
                    Me.State.MyBO.MyDropDownParentCode = IssueTypeCode
                    Me.State.MyBO.MyDropDownListItemId = New Guid(Me.Grid.Items(e.Item.ItemIndex).Cells(GRID_COL_IDX).Text)
                    Me.State.MyBO.MyDropDownOldCode = Me.State.MyBO.Code

                ElseIf (e.CommandName = DELETE_COMMAND) Then

                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    Grid.SelectedIndex = NO_ROW_SELECTED_INDEX

                    Me.State.Id = New Guid(Me.Grid.Items(e.Item.ItemIndex).Cells(GRID_COL_IDX).Text)
                    Me.State.MyBO = New IssueType(Me.State.Id)

                    'populate dropdown values
                    Me.State.MyBO.MyDropDownParentCode = IssueTypeCode
                    Me.State.MyBO.MyDropDownNewItemCode = Me.Grid.Items(e.Item.ItemIndex).Cells(GRID_COL_CODE_IDX).Text
                    Me.State.MyBO.MyDropDownNewItemDesc = Me.Grid.Items(e.Item.ItemIndex).Cells(GRID_COL_DESCRIPTION_IDX).Text
                    Me.State.MyBO.MyDropDownOldCode = Me.State.MyBO.Code

                    Try
                        Me.State.MyBO.Delete()
                        Me.State.MyBO.Save()
                        Me.State.PageIndex = Grid.CurrentPageIndex
                        Me.State.IsAfterSave = True
                        Me.AddInfoMsg(MSG_RECORD_DELETED_OK)
                        Me.State.SearchDV = Nothing
                        Me.ReturnFromEditing()
                    Catch ex As Exception
                        Me.State.MyBO.RejectChanges()
                        Throw
                    End Try
                    Me.State.PageIndex = Grid.CurrentPageIndex
                End If

            Catch ex As Exception

                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub


        Protected Sub ItemBound(ByVal source As Object, ByVal e As DataGridItemEventArgs) Handles Grid.ItemDataBound
            BaseItemBound(source, e)
        End Sub

        Protected Sub ItemCreated(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub GridPageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged

            Try
                If (Not (Me.State.IsEditMode)) Then
                    Me.State.PageIndex = e.NewPageIndex
                    Me.Grid.CurrentPageIndex = Me.State.PageIndex
                    Me.PopulateGrid()
                    Me.Grid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub GridPageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.State.SelectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub GridSortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles Grid.SortCommand

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
                'To handle the requirement of always going to the FIRST page on the Grid whenever the user switches the sorting criterion
                Me.State.Id = Guid.Empty
                Me.State.PageIndex = 0

                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "CODE", Me.Grid.Columns(GRID_COL_CODE_IDX))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "DESCRIPTION", Me.Grid.Columns(GRID_COL_DESCRIPTION_IDX))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "ISSYSTEMGENERATED", Me.Grid.Columns(GRID_COL_IS_SYSTEM_GENERATED_IDX))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "ISSELFCLEANING", Me.Grid.Columns(GRID_COL_IS_SELF_CLEANING_IDX))
            Me.ClearGridHeadersAndLabelsErrSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(ByVal thisGrid As DataGrid, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim desc As TextBox = CType(thisGrid.Items(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(desc)
        End Sub

#End Region
    End Class

End Namespace

