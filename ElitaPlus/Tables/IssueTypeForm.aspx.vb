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
                IsEditing = (Grid.EditItemIndex > NO_ROW_SELECTED_INDEX)
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

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
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

        Private Sub SearchButton_Click(sender As System.Object, e As System.EventArgs) Handles SearchButton.Click
            Try
                State.Id = Guid.Empty
                State.PageIndex = 0
                State.IsGridVisible = True
                State.SearchDV = Nothing
                State.HasDataChanged = False
                State.SearchClick = True
                PopulateGrid()

                State.PageIndex = Grid.CurrentPageIndex
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub ClearButton_Click(sender As System.Object, e As System.EventArgs) Handles ClearButton.Click
            ClearSearchCriteria()
        End Sub

        Private Sub ClearSearchCriteria()
            Try
                moDescriptionText.Text = String.Empty
                moCodeText.Text = String.Empty
                ReturnFromEditing()

                'Update Page State
                With State
                    .DescriptionMask = moDescriptionText.Text
                    .CodeMask = moCodeText.Text
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
                AddNewBlankRow()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub


        Private Sub SaveButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles SaveButton_WRITE.Click
            Try
                PopulateBOFromForm()
                If (State.MyBO.IsDirty) Then
                    State.MyBO.Save()
                    State.IsAfterSave = True
                    State.AddingNewRow = False
                    AddInfoMsg(MSG_RECORD_SAVED_OK)
                    State.SearchDV = Nothing
                    ReturnFromEditing()
                Else
                    AddInfoMsg(MSG_RECORD_NOT_SAVED)
                    ReturnFromEditing()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            PopulateGrid()
            SetGridControls(Grid, False)

        End Sub


        Private Sub CancelButton_Click(sender As System.Object, e As System.EventArgs) Handles CancelButton.Click
            Try
                Grid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                State.Canceling = True
                If (State.AddingNewRow) Then
                    State.AddingNewRow = False
                    State.SearchDV = Nothing
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

                    SetDefaultButton(moCodeText, SearchButton)
                    SetDefaultButton(moDescriptionText, SearchButton)

                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    SetGridItemStyleColor(Grid)
                    State.PageIndex = 0
                    If State.MyBO Is Nothing Then
                        State.MyBO = New IssueType
                    End If
                    SetButtonsState()

                    TranslateGridControls(Grid)
                    PopulateYesNo()
                End If
                BindBoPropertiesToGridHeaders()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)

        End Sub

        Private Sub PopulateYesNo()
            Dim yesNoDV As DataView = LookupListNew.DropdownLookupList(YESNO, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
            State.YesNoDV = yesNoDV
        End Sub

        Private Sub PopulateGrid()
            Try
                If ((State.SearchDV Is Nothing) OrElse (State.HasDataChanged)) Then
                    State.Code = moCodeText.Text
                    State.Description = moDescriptionText.Text
                    State.SearchDV = IssueType.GetList(State.Code, State.Description)
                End If

                State.SearchDV.Sort = State.SortExpression

                If (State.IsAfterSave) Then
                    State.IsAfterSave = False
                    SetPageAndSelectedIndexFromGuid(State.SearchDV, State.Id, Grid, State.PageIndex)
                ElseIf (State.IsEditMode) Then
                    SetPageAndSelectedIndexFromGuid(State.SearchDV, State.Id, Grid, State.PageIndex, State.IsEditMode)
                Else
                    SetPageAndSelectedIndexFromGuid(State.SearchDV, State.Id, Grid, State.PageIndex)
                End If

                Grid.AutoGenerateColumns = False
                Grid.Columns(GRID_COL_CODE_IDX).SortExpression = IssueType.IssueTypeSearchDV.COL_NAME_CODE
                Grid.Columns(GRID_COL_DESCRIPTION_IDX).SortExpression = IssueType.IssueTypeSearchDV.COL_NAME_DESCRIPTION
                Grid.Columns(GRID_COL_IS_SYSTEM_GENERATED_IDX).SortExpression = IssueType.IssueTypeSearchDV.COL_NAME_IS_SYSTEM_GENERATED_DESC
                Grid.Columns(GRID_COL_IS_SELF_CLEANING_IDX).SortExpression = IssueType.IssueTypeSearchDV.COL_NAME_IS_SELF_CLEANING_DESC

                If State.SearchClick Then
                    ValidSearchResultCount(State.SearchDV.Count, True)
                    State.SearchClick = False
                End If
                SortAndBindGrid()

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Function GetGridDataView() As DataView
            With State
                Return IssueType.GetList(State.Code, State.Description)
            End With

        End Function

        Private Sub SetStateProperties()
            State.DescriptionMask = moDescriptionText.Text
            State.CodeMask = moCodeText.Text
        End Sub

        Private Sub SortAndBindGrid()
            State.PageIndex = Grid.CurrentPageIndex
            Grid.DataSource = State.SearchDV
            HighLightSortColumn(Grid, State.SortExpression)
            Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
            Session("recCount") = State.SearchDV.Count
            If Grid.Visible Then
                If (State.AddingNewRow) Then
                    lblRecordCount.Text = (State.SearchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    lblRecordCount.Text = State.SearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        End Sub


        Private Sub AddNewBlankRow()
            Dim dv As DataView

            State.SearchDV = GetGridDataView()
            State.MyBO = New IssueType
            State.Id = State.MyBO.Id
            State.SearchDV = IssueType.GetNewDataViewRow(State.SearchDV, State.Id, State.MyBO)

            'populate dropdown values
            State.MyBO.MyDropDownParentCode = IssueTypeCode

            Grid.DataSource = State.SearchDV
            SetGridControls(Grid, False)

            Grid.AutoGenerateColumns = False
            SetPageAndSelectedIndexFromGuid(State.SearchDV, State.Id, Grid, State.PageIndex, State.IsEditMode)

            Grid.DataBind()
            State.PageIndex = Grid.CurrentPageIndex

            'Set focus on the BusinessUnit TextBox for the EditItemIndex row
            SetFocusOnEditableFieldInGrid(Grid, GRID_COL_DESCRIPTION_IDX, DESCRIPTION_CONTROL_NAME, Grid.EditItemIndex)

            SetButtonsState()
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        End Sub

        Private Sub PopulateBOFromForm()
            Try
                With State.MyBO
                    'Populate bo properties for the dropdown
                    .MyDropDownParentCode = IssueTypeCode
                    .MyDropDownNewItemCode = CType(Grid.Items(Grid.EditItemIndex).Cells(GRID_COL_CODE_IDX).FindControl(CODE_CONTROL_NAME), TextBox).Text
                    .MyDropDownNewItemDesc = CType(Grid.Items(Grid.EditItemIndex).Cells(GRID_COL_DESCRIPTION_IDX).FindControl(DESCRIPTION_CONTROL_NAME), TextBox).Text

                    .Description = CType(Grid.Items(Grid.EditItemIndex).Cells(GRID_COL_DESCRIPTION_IDX).FindControl(DESCRIPTION_CONTROL_NAME), TextBox).Text
                    .Code = CType(Grid.Items(Grid.EditItemIndex).Cells(GRID_COL_CODE_IDX).FindControl(CODE_CONTROL_NAME), TextBox).Text
                    .IsSystemGenerated = GetSelectedItem(CType(Grid.Items(Grid.EditItemIndex).Cells(GRID_COL_IS_SYSTEM_GENERATED_IDX).FindControl(IS_SYS_GEN_CONTROL_NAME), DropDownList))
                    .IsSelfCleaning = GetSelectedItem(CType(Grid.Items(Grid.EditItemIndex).Cells(GRID_COL_CODE_IDX).FindControl(IS_SELF_CLEAN_CONTROL_NAME), DropDownList))
                End With

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub ReturnFromEditing()

            Grid.EditItemIndex = NO_ROW_SELECTED_INDEX
            If Grid.PageCount = 0 Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, Grid, False)
            Else
                ControlMgr.SetVisibleControl(Me, Grid, True)
            End If
            SetGridControls(Grid, True)
            State.IsEditMode = False
            PopulateGrid()
            State.PageIndex = Grid.CurrentPageIndex
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

#End Region

#Region " Datagrid Related "

        'The Binding Logic is here
        Private Sub GridItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
            Dim itemType As ListItemType = e.Item.ItemType
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

            If (itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem) Then
                Dim dRow() As DataRow
                e.Item.Cells(GRID_COL_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(IssueType.IssueTypeSearchDV.COL_NAME_ISSUE_TYPE_ID), Byte()))
                e.Item.Cells(GRID_COL_CODE_IDX).Text = dvRow(IssueType.IssueTypeSearchDV.COL_NAME_CODE).ToString
                e.Item.Cells(GRID_COL_DESCRIPTION_IDX).Text = dvRow(IssueType.IssueTypeSearchDV.COL_NAME_DESCRIPTION).ToString

                dRow = State.YesNoDV.Table.Select("code='" & dvRow(IssueType.IssueTypeSearchDV.COL_NAME_IS_SYSTEM_GENERATED_DESC).ToString & "' and language_id='" & GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.LanguageId) & "'")
                If (dRow IsNot Nothing AndAlso dRow.Length > 0) Then
                    e.Item.Cells(GRID_COL_IS_SYSTEM_GENERATED_IDX).Text = CType(dRow(0).Item("Description"), String).ToString
                End If
                dRow = State.YesNoDV.Table.Select("code='" & dvRow(IssueType.IssueTypeSearchDV.COL_NAME_IS_SELF_CLEANING_DESC).ToString & "' and language_id='" & GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.LanguageId) & "'")
                If (dRow IsNot Nothing AndAlso dRow.Length > 0) Then
                    e.Item.Cells(GRID_COL_IS_SELF_CLEANING_IDX).Text = CType(dRow(0).Item("Description"), String).ToString
                End If

            ElseIf (itemType = ListItemType.EditItem) Then

                e.Item.Cells(GRID_COL_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(IssueType.IssueTypeSearchDV.COL_NAME_ISSUE_TYPE_ID), Byte()))
                CType(e.Item.Cells(GRID_COL_DESCRIPTION_IDX).FindControl(DESCRIPTION_CONTROL_NAME), TextBox).Text = State.MyBO.Description
                CType(e.Item.Cells(GRID_COL_CODE_IDX).FindControl(CODE_CONTROL_NAME), TextBox).Text = State.MyBO.Code

                ' BindListControlToDataView(CType(e.Item.Cells(GRID_COL_IS_SYSTEM_GENERATED_IDX).FindControl(IS_SYS_GEN_CONTROL_NAME), DropDownList), LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , False)
                Dim yesNoLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())
                CType(e.Item.Cells(GRID_COL_IS_SYSTEM_GENERATED_IDX).FindControl(IS_SYS_GEN_CONTROL_NAME), DropDownList).Populate(yesNoLkl, New PopulateOptions())
                If Not State.MyBO.IsSystemGenerated.Equals(Guid.Empty) Then
                    SetSelectedItem(CType(e.Item.Cells(GRID_COL_IS_SYSTEM_GENERATED_IDX).FindControl(IS_SYS_GEN_CONTROL_NAME), DropDownList), State.MyBO.IsSystemGenerated())
                End If

                'BindListControlToDataView(CType(e.Item.Cells(GRID_COL_IS_SELF_CLEANING_IDX).FindControl(IS_SELF_CLEAN_CONTROL_NAME), DropDownList), LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , False)
                CType(e.Item.Cells(GRID_COL_IS_SELF_CLEANING_IDX).FindControl(IS_SELF_CLEAN_CONTROL_NAME), DropDownList).Populate(yesNoLkl, New PopulateOptions())
                If Not State.MyBO.IsSelfCleaning.Equals(Guid.Empty) Then
                    SetSelectedItem(CType(e.Item.Cells(GRID_COL_IS_SELF_CLEANING_IDX).FindControl(IS_SELF_CLEAN_CONTROL_NAME), DropDownList), State.MyBO.IsSelfCleaning())
                End If
            End If

        End Sub

        Protected Sub ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)

            Try

                If (e.CommandName = EDIT_COMMAND) Then
                    'Set the IsEditMode flag to TRUE
                    State.IsEditMode = True

                    State.Id = New Guid(Grid.Items(e.Item.ItemIndex).Cells(GRID_COL_IDX).Text)
                    State.MyBO = New IssueType(State.Id)
                    PopulateGrid()
                    State.PageIndex = Grid.CurrentPageIndex

                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(Grid, False)

                    'Set focus on the Description TextBox for the EditItemIndex row
                    SetFocusOnEditableFieldInGrid(Grid, GRID_COL_DESCRIPTION_IDX, DESCRIPTION_CONTROL_NAME, e.Item.ItemIndex)
                    SetButtonsState()

                    'populate dropdown values
                    State.MyBO.MyDropDownParentCode = IssueTypeCode
                    State.MyBO.MyDropDownListItemId = New Guid(Grid.Items(e.Item.ItemIndex).Cells(GRID_COL_IDX).Text)
                    State.MyBO.MyDropDownOldCode = State.MyBO.Code

                ElseIf (e.CommandName = DELETE_COMMAND) Then

                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    Grid.SelectedIndex = NO_ROW_SELECTED_INDEX

                    State.Id = New Guid(Grid.Items(e.Item.ItemIndex).Cells(GRID_COL_IDX).Text)
                    State.MyBO = New IssueType(State.Id)

                    'populate dropdown values
                    State.MyBO.MyDropDownParentCode = IssueTypeCode
                    State.MyBO.MyDropDownNewItemCode = Grid.Items(e.Item.ItemIndex).Cells(GRID_COL_CODE_IDX).Text
                    State.MyBO.MyDropDownNewItemDesc = Grid.Items(e.Item.ItemIndex).Cells(GRID_COL_DESCRIPTION_IDX).Text
                    State.MyBO.MyDropDownOldCode = State.MyBO.Code

                    Try
                        State.MyBO.Delete()
                        State.MyBO.Save()
                        State.PageIndex = Grid.CurrentPageIndex
                        State.IsAfterSave = True
                        AddInfoMsg(MSG_RECORD_DELETED_OK)
                        State.SearchDV = Nothing
                        ReturnFromEditing()
                    Catch ex As Exception
                        State.MyBO.RejectChanges()
                        Throw
                    End Try
                    State.PageIndex = Grid.CurrentPageIndex
                End If

            Catch ex As Exception

                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub


        Protected Sub ItemBound(source As Object, e As DataGridItemEventArgs) Handles Grid.ItemDataBound
            BaseItemBound(source, e)
        End Sub

        Protected Sub ItemCreated(sender As Object, e As DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub GridPageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged

            Try
                If (Not (State.IsEditMode)) Then
                    State.PageIndex = e.NewPageIndex
                    Grid.CurrentPageIndex = State.PageIndex
                    PopulateGrid()
                    Grid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub GridPageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                State.SelectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub GridSortCommand(source As Object, e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles Grid.SortCommand

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
                'To handle the requirement of always going to the FIRST page on the Grid whenever the user switches the sorting criterion
                State.Id = Guid.Empty
                State.PageIndex = 0

                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()
            BindBOPropertyToGridHeader(State.MyBO, "CODE", Grid.Columns(GRID_COL_CODE_IDX))
            BindBOPropertyToGridHeader(State.MyBO, "DESCRIPTION", Grid.Columns(GRID_COL_DESCRIPTION_IDX))
            BindBOPropertyToGridHeader(State.MyBO, "ISSYSTEMGENERATED", Grid.Columns(GRID_COL_IS_SYSTEM_GENERATED_IDX))
            BindBOPropertyToGridHeader(State.MyBO, "ISSELFCLEANING", Grid.Columns(GRID_COL_IS_SELF_CLEANING_IDX))
            ClearGridHeadersAndLabelsErrSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(thisGrid As DataGrid, cellPosition As Integer, controlName As String, itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim desc As TextBox = CType(thisGrid.Items(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(desc)
        End Sub

#End Region
    End Class

End Namespace

