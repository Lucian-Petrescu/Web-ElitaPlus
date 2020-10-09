Option Strict On
Option Explicit On

Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms


Partial Class TurnAroundTimeRangeForm
    Inherits ElitaPlusSearchPage

    Protected WithEvents ErrController As ErrorController

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
            IsEditing = (Grid.EditIndex > NO_ROW_SELECTED_INDEX)
        End Get
    End Property

#End Region

#Region "Page State"
    Class MyState
        Public PageIndex As Integer = 0
        Public TurnAroundTimeRange As BusinessObjectsNew.TurnAroundTimeRange

        Public TurnAroundTimeRangeId As Guid
        Public IsGridVisible As Boolean
        Public IsAfterSave As Boolean
        Public IsEditMode As Boolean
        Public AddingNewRow As Boolean
        Public Canceling As Boolean
        Public searchDV As DataView = Nothing
        Public SortExpression As String = TurnAroundTimeRange.COL_NAME_CODE
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public bnoRow As Boolean = False
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
    Private Const MIN_DAYS_COL As Integer = 5
    Private Const MAX_DAYS_COL As Integer = 6
    Private Const COLOR_COL As Integer = 7

    Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
    Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
    Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
    Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

    Private Const DESCRIPTION_CONTROL_NAME As String = "DescriptionTextBox"
    Private Const CODE_CONTROL_NAME As String = "CodeTextBox"
    Private Const MIN_DAYS_CONTROL_NAME As String = "MinDaysTextBox"
    Private Const MAX_DAYS_CONTROL_NAME As String = "MaxDaysTextBox"
    Private Const COLOR_CONTROL_NAME As String = "cboColor"
    Private Const ID_CONTROL_NAME As String = "IdLabel"

    Private Const EDIT_COMMAND As String = "EditRecord"
    Private Const DELETE_COMMAND As String = "DeleteRecord"
    Private Const SORT_COMMAND As String = "Sort"

    'Private Const EDIT As String = "Edit"
    'Private Const DELETE As String = "Delete"

    Private Const NO_ROW_SELECTED_INDEX As Integer = -1

#End Region

#Region "Button Click Handlers"

    Private Sub SearchButton_Click(sender As System.Object, e As System.EventArgs) Handles SearchButton.Click
        Try
            State.IsGridVisible = True
            State.searchDV = Nothing
            PopulateGrid()
            'Me.State.PageIndex = Grid.CurrentPageIndex
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Private Sub NewButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles NewButton_WRITE.Click

        Try
            State.IsEditMode = True
            State.IsGridVisible = True
            State.AddingNewRow = True
            AddNewTurnAroundTimeRange()
            SetGridControls(Grid, False)
            SetButtonsState()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Private Sub SaveButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles SaveButton_WRITE.Click

        Try
            PopulateBOFromForm()
            If (State.TurnAroundTimeRange.IsDirty) Then
                State.TurnAroundTimeRange.Save()
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
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Private Sub CancelButton_Click(sender As System.Object, e As System.EventArgs) Handles CancelButton.Click

        Try
            Grid.SelectedIndex = NO_ITEM_SELECTED_INDEX
            State.Canceling = True
            If (State.AddingNewRow) Then
                State.AddingNewRow = False
                State.searchDV = Nothing
            End If
            ReturnFromEditing()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

#End Region


#Region "Private Methods"

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        'Put user code to initialize the page here
        Try
            ErrController.Clear_Hide()
            If Not Page.IsPostBack Then
                SortDirection = State.SortExpression
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                TranslateGridHeader(Grid)
                TranslateGridControls(Grid)
                SetGridItemStyleColor(Grid)
                State.PageIndex = 0
                SetButtonsState()
            End If
            BindBoPropertiesToGridHeaders()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
        ShowMissingTranslations(ErrController)
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
                SetPageAndSelectedIndexFromGuid(State.searchDV, State.TurnAroundTimeRangeId, Grid, State.PageIndex)
            ElseIf (State.IsEditMode) Then
                SetPageAndSelectedIndexFromGuid(State.searchDV, State.TurnAroundTimeRangeId, Grid, State.PageIndex, State.IsEditMode)
            Else
                SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, Grid, State.PageIndex)
            End If

            Grid.AutoGenerateColumns = False
            SortAndBindGrid()


        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Private Function GetDV() As DataView

        Dim dv As DataView

        State.searchDV = Assurant.ElitaPlus.BusinessObjectsNew.TurnAroundTimeRange.LoadList()
        State.searchDV.Sort = Grid.DataMember()

        Return (State.searchDV)

    End Function



    Private Sub AddNewTurnAroundTimeRange()

        State.searchDV = Assurant.ElitaPlus.BusinessObjectsNew.TurnAroundTimeRange.LoadList()

        State.TurnAroundTimeRange = New Assurant.ElitaPlus.BusinessObjectsNew.TurnAroundTimeRange
        State.TurnAroundTimeRangeId = State.TurnAroundTimeRange.Id

        State.searchDV = State.TurnAroundTimeRange.GetNewDataViewRow(State.searchDV, State.TurnAroundTimeRangeId)

        Grid.DataSource = State.searchDV
        SetGridControls(Grid, False)
        SetPageAndSelectedIndexFromGuid(State.searchDV, State.TurnAroundTimeRangeId, Grid, State.PageIndex, State.IsEditMode)

        Grid.AutoGenerateColumns = False
        Grid.Columns(CODE_COL).SortExpression = TurnAroundTimeRange.COL_NAME_CODE
        Grid.Columns(DESCRIPTION_COL).SortExpression = TurnAroundTimeRange.COL_NAME_DESCRIPTION
        SortAndBindGrid()

        'Grid.DataBind()

        'Me.State.PageIndex = Grid.PageIndex



        ''Set focus on the Description TextBox for the EditItemIndex row
        'Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.CODE_COL, Me.CODE_CONTROL_NAME, Me.Grid.EditIndex)

        'Me.PopulateFormFromBO()

        ''Me.TranslateGridControls(Grid)
        'Me.SetButtonsState()
        'ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
    End Sub

    Private Sub SortAndBindGrid()
        Dim dv As New DataView
        State.PageIndex = Grid.PageIndex

        If (State.searchDV.Count = 0) Then

            dv = Assurant.ElitaPlus.BusinessObjectsNew.TurnAroundTimeRange.LoadList()

            State.bnoRow = True
            dv = TurnAroundTimeRange.getEmptyList(dv)
            Grid.DataSource = dv
            Grid.DataBind()
            Grid.Rows(0).Visible = False


        Else
            State.bnoRow = False
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
            If (State.AddingNewRow) Then
                lblRecordCount.Text = (State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            Else
                lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End If
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
    End Sub

    Private Sub PopulateBOFromForm()

        Try
            With State.TurnAroundTimeRange


                .Code = CType(Grid.Rows(Grid.EditIndex).Cells(CODE_COL).FindControl(CODE_CONTROL_NAME), TextBox).Text
                .Description = CType(Grid.Rows(Grid.EditIndex).Cells(CODE_COL).FindControl(DESCRIPTION_CONTROL_NAME), TextBox).Text

                Dim minDaysString As String = CType(Grid.Rows(Grid.EditIndex).Cells(MIN_DAYS_COL).FindControl(MIN_DAYS_CONTROL_NAME), TextBox).Text
                Try
                    .MinDays = CType(minDaysString, LongType)
                Catch ex As Exception
                    Throw New GUIException(Message.MSG_INVALID_MIN_MAX_VALUE, Assurant.ElitaPlus.Common.ErrorCodes.MIN_VALUE_MUST_BE_FROM_0_TO_9998)
                End Try

                Dim MaxDaysString As String = CType(Grid.Rows(Grid.EditIndex).Cells(MAX_DAYS_COL).FindControl(MAX_DAYS_CONTROL_NAME), TextBox).Text
                Try
                    .MaxDays = CType(MaxDaysString, LongType)
                Catch ex As Exception
                    Throw New GUIException(Message.MSG_INVALID_MIN_MAX_VALUE, Assurant.ElitaPlus.Common.ErrorCodes.MAX_VALUE_MUST_BE_FROM_1_TO_9999)
                End Try

                .ColorId = GetSelectedItem(CType(Grid.Rows(Grid.EditIndex).Cells(COLOR_COL).FindControl(COLOR_CONTROL_NAME), DropDownList))
                .CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

            End With
        Catch ex As Exception
            Throw ex 'Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Private Sub PopulateFormFromBO()

        Dim gridRowIdx As Integer = Grid.EditIndex
        Try
            With State.TurnAroundTimeRange
                If .Description IsNot Nothing Then
                    CType(Grid.Rows(gridRowIdx).Cells(DESCRIPTION_COL).FindControl(DESCRIPTION_CONTROL_NAME), TextBox).Text = .Description
                End If
                'If Not .Code Is Nothing Then
                '    CType(Me.Grid.Items(gridRowIdx).Cells(Me.CODE_COL).FindControl(Me.CODE_CONTROL_NAME), TextBox).Text = .Code
                'End If
                CType(Grid.Rows(gridRowIdx).Cells(ID_COL).FindControl(ID_CONTROL_NAME), Label).Text = .Id.ToString
            End With
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Private Sub ReturnFromEditing()

        Grid.EditIndex = NO_ROW_SELECTED_INDEX

        If Grid.PageCount = 0 Then
            'if returning to the "1st time in" screen
            ControlMgr.SetVisibleControl(Me, Grid, False)
        Else
            ControlMgr.SetVisibleControl(Me, Grid, True)
        End If

        State.IsEditMode = False
        PopulateGrid()
        State.PageIndex = Grid.PageIndex
        SetButtonsState()

    End Sub

    Private Sub SetButtonsState()

        If (State.IsEditMode) Then
            ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, True)
            ControlMgr.SetVisibleControl(Me, CancelButton, True)
            ControlMgr.SetVisibleControl(Me, NewButton_WRITE, False)
            ControlMgr.SetEnableControl(Me, SearchButton, False)
            MenuEnabled = False
            If (cboPageSize.Visible) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, False)
            End If
        Else
            ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, False)
            ControlMgr.SetVisibleControl(Me, CancelButton, False)
            ControlMgr.SetVisibleControl(Me, NewButton_WRITE, True)
            ControlMgr.SetEnableControl(Me, SearchButton, True)
            MenuEnabled = True
            If (cboPageSize.Visible) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, True)
            End If
        End If

    End Sub

    Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
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

    Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging

        Try
            If (Not (State.IsEditMode)) Then
                State.PageIndex = e.NewPageIndex
                Grid.PageIndex = State.PageIndex
                PopulateGrid()
                Grid.SelectedIndex = NO_ITEM_SELECTED_INDEX
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Protected Sub ItemCommand(source As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)

        Try
            Dim index As Integer

            If (e.CommandName = EDIT_COMMAND) Then
                'Do the Edit here
                index = CInt(e.CommandArgument)
                'Set the IsEditMode flag to TRUE
                State.IsEditMode = True

                State.TurnAroundTimeRangeId = New Guid(CType(Grid.Rows(index).Cells(ID_COL).FindControl(ID_CONTROL_NAME), Label).Text)

                State.TurnAroundTimeRange = New Assurant.ElitaPlus.BusinessObjectsNew.TurnAroundTimeRange(State.TurnAroundTimeRangeId)

                PopulateGrid()

                State.PageIndex = Grid.PageIndex

                'Disable all Edit and Delete icon buttons on the Grid
                SetGridControls(Grid, False)

                'Set focus on the Description TextBox for the EditItemIndex row
                SetFocusOnEditableFieldInGrid(Grid, DESCRIPTION_COL, DESCRIPTION_CONTROL_NAME, index)

                PopulateFormFromBO()

                SetButtonsState()

            ElseIf (e.CommandName = DELETE_COMMAND) Then
                'Do the delete here
                index = CInt(e.CommandArgument)
                'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                Grid.SelectedIndex = NO_ROW_SELECTED_INDEX

                'Save the Id in the Session

                State.TurnAroundTimeRangeId = New Guid(CType(Grid.Rows(index).Cells(ID_COL).FindControl(ID_CONTROL_NAME), Label).Text)

                State.TurnAroundTimeRange = New Assurant.ElitaPlus.BusinessObjectsNew.TurnAroundTimeRange(State.TurnAroundTimeRangeId)
                Try
                    State.TurnAroundTimeRange.Delete()
                    'Call the Save() method in the TurnAroundTimeRange Business Object here
                    State.TurnAroundTimeRange.Save()
                Catch ex As Exception
                    State.TurnAroundTimeRange.RejectChanges()
                    Throw ex
                End Try

                State.PageIndex = Grid.PageIndex

                'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
                State.IsAfterSave = True
                State.searchDV = Nothing
                PopulateGrid()
                State.PageIndex = Grid.PageIndex

            ElseIf ((e.CommandName = SORT_COMMAND) AndAlso Not (IsEditing)) Then


            End If

        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Protected Sub ItemBound(source As Object, e As GridViewRowEventArgs) Handles Grid.RowDataBound

        Try
            BaseItemBound(source, e)
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub
    'The Binding Logic is here
    Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

        If dvRow IsNot Nothing AndAlso Not State.bnoRow Then

            If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                CType(e.Row.Cells(ID_COL).FindControl(ID_CONTROL_NAME), Label).Text = GetGuidStringFromByteArray(CType(dvRow("turn_around_time_range_id"), Byte()))


                If (State.IsEditMode = True _
                        AndAlso State.TurnAroundTimeRangeId.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow("turn_around_time_range_id"), Byte())))) Then
                    CType(e.Row.Cells(CODE_COL).FindControl(CODE_CONTROL_NAME), TextBox).Text = dvRow("Code").ToString
                    CType(e.Row.Cells(DESCRIPTION_COL).FindControl(DESCRIPTION_CONTROL_NAME), TextBox).Text = dvRow("DESCRIPTION").ToString
                    CType(e.Row.Cells(MIN_DAYS_COL).FindControl(MIN_DAYS_CONTROL_NAME), TextBox).Text = dvRow("Min_Days").ToString
                    CType(e.Row.Cells(MAX_DAYS_COL).FindControl(MAX_DAYS_CONTROL_NAME), TextBox).Text = dvRow("Max_Days").ToString

                    ' Me.BindListControlToDataView(CType(e.Row.Cells(Me.COLOR_COL).FindControl(Me.COLOR_CONTROL_NAME), DropDownList), LookupListNew.GetColorLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                    CType(e.Row.Cells(COLOR_COL).FindControl(COLOR_CONTROL_NAME), DropDownList).Populate(CommonConfigManager.Current.ListManager.GetList("COLORS", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                    {
                     .AddBlankItem = True
                   })
                    SetSelectedItem(CType(e.Row.Cells(COLOR_COL).FindControl(COLOR_CONTROL_NAME), DropDownList), State.TurnAroundTimeRange.ColorId)

                    If State.AddingNewRow Then
                        CType(e.Row.Cells(MIN_DAYS_COL).FindControl(MIN_DAYS_CONTROL_NAME), TextBox).Enabled = True
                        CType(e.Row.Cells(MAX_DAYS_COL).FindControl(MAX_DAYS_CONTROL_NAME), TextBox).Enabled = True
                    ElseIf e.Row.RowIndex = 0 Then 'allow only the min value on the first record and the max value value of the last record to be editied.
                        CType(e.Row.Cells(MIN_DAYS_COL).FindControl(MIN_DAYS_CONTROL_NAME), TextBox).Enabled = True
                    ElseIf e.Row.RowIndex = State.searchDV.Count - 1 Then
                        CType(e.Row.Cells(MAX_DAYS_COL).FindControl(MAX_DAYS_CONTROL_NAME), TextBox).Enabled = True
                    End If

                Else
                    CType(e.Row.Cells(CODE_COL).FindControl("CodeLabel"), Label).Text = dvRow("Code").ToString
                    CType(e.Row.Cells(DESCRIPTION_COL).FindControl("DescriptionLabel"), Label).Text = dvRow("DESCRIPTION").ToString
                    CType(e.Row.Cells(MIN_DAYS_COL).FindControl("MinDaysLabel"), Label).Text = dvRow("Min_Days").ToString
                    CType(e.Row.Cells(MAX_DAYS_COL).FindControl("MaxDaysLabel"), Label).Text = dvRow("Max_days").ToString
                    CType(e.Row.Cells(COLOR_COL).FindControl("ColorLabel"), Label).Text = dvRow("color").ToString
                End If
            End If
        End If
    End Sub

    Protected Sub ItemCreated(sender As Object, e As GridViewRowEventArgs)
        BaseItemCreated(sender, e)


    End Sub

    Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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
            State.SortExpression = SortDirection
            State.PageIndex = 0
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Protected Sub BindBoPropertiesToGridHeaders()
        BindBOPropertyToGridHeader(State.TurnAroundTimeRange, "Code", Grid.Columns(CODE_COL))
        BindBOPropertyToGridHeader(State.TurnAroundTimeRange, "Description", Grid.Columns(DESCRIPTION_COL))
        BindBOPropertyToGridHeader(State.TurnAroundTimeRange, "MinDays", Grid.Columns(MIN_DAYS_COL))
        BindBOPropertyToGridHeader(State.TurnAroundTimeRange, "MaxDays", Grid.Columns(MAX_DAYS_COL))
        BindBOPropertyToGridHeader(State.TurnAroundTimeRange, "ColorId", Grid.Columns(COLOR_COL))

        ClearGridViewHeadersAndLabelsErrSign()
    End Sub

    Private Sub SetFocusOnEditableFieldInGrid(grid As GridView, cellPosition As Integer, controlName As String, itemIndex As Integer)
        'Set focus on the Description TextBox for the EditItemIndex row
        Dim desc As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
        SetFocus(desc)
    End Sub

#End Region

End Class
