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
            IsEditing = (Me.Grid.EditIndex > NO_ROW_SELECTED_INDEX)
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

    Private Sub SearchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchButton.Click
        Try
            Me.State.IsGridVisible = True
            Me.State.searchDV = Nothing
            PopulateGrid()
            'Me.State.PageIndex = Grid.CurrentPageIndex
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Private Sub NewButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewButton_WRITE.Click

        Try
            Me.State.IsEditMode = True
            Me.State.IsGridVisible = True
            Me.State.AddingNewRow = True
            AddNewTurnAroundTimeRange()
            Me.SetGridControls(Me.Grid, False)
            SetButtonsState()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Private Sub SaveButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton_WRITE.Click

        Try
            PopulateBOFromForm()
            If (Me.State.TurnAroundTimeRange.IsDirty) Then
                Me.State.TurnAroundTimeRange.Save()
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
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click

        Try
            Me.Grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
            Me.State.Canceling = True
            If (Me.State.AddingNewRow) Then
                Me.State.AddingNewRow = False
                Me.State.searchDV = Nothing
            End If
            ReturnFromEditing()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

#End Region


#Region "Private Methods"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Put user code to initialize the page here
        Try
            ErrController.Clear_Hide()
            If Not Page.IsPostBack Then
                Me.SortDirection = Me.State.SortExpression
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                Me.TranslateGridHeader(Me.Grid)
                Me.TranslateGridControls(Me.Grid)
                Me.SetGridItemStyleColor(Grid)
                Me.State.PageIndex = 0
                SetButtonsState()
            End If
            BindBoPropertiesToGridHeaders()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
        Me.ShowMissingTranslations(ErrController)
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
                Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.TurnAroundTimeRangeId, Me.Grid, Me.State.PageIndex)
            ElseIf (Me.State.IsEditMode) Then
                Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.TurnAroundTimeRangeId, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
            Else
                Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.Grid, Me.State.PageIndex)
            End If

            Me.Grid.AutoGenerateColumns = False
            Me.SortAndBindGrid()


        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Private Function GetDV() As DataView

        Dim dv As DataView

        Me.State.searchDV = Assurant.ElitaPlus.BusinessObjectsNew.TurnAroundTimeRange.LoadList()
        Me.State.searchDV.Sort = Grid.DataMember()

        Return (Me.State.searchDV)

    End Function



    Private Sub AddNewTurnAroundTimeRange()

        Me.State.searchDV = Assurant.ElitaPlus.BusinessObjectsNew.TurnAroundTimeRange.LoadList()

        Me.State.TurnAroundTimeRange = New Assurant.ElitaPlus.BusinessObjectsNew.TurnAroundTimeRange
        Me.State.TurnAroundTimeRangeId = Me.State.TurnAroundTimeRange.Id

        Me.State.searchDV = Me.State.TurnAroundTimeRange.GetNewDataViewRow(Me.State.searchDV, Me.State.TurnAroundTimeRangeId)

        Grid.DataSource = Me.State.searchDV
        SetGridControls(Me.Grid, False)
        Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.TurnAroundTimeRangeId, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)

        Me.Grid.AutoGenerateColumns = False
        Me.Grid.Columns(Me.CODE_COL).SortExpression = TurnAroundTimeRange.COL_NAME_CODE
        Me.Grid.Columns(Me.DESCRIPTION_COL).SortExpression = TurnAroundTimeRange.COL_NAME_DESCRIPTION
        Me.SortAndBindGrid()

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
        Me.State.PageIndex = Me.Grid.PageIndex

        If (Me.State.searchDV.Count = 0) Then

            dv = Assurant.ElitaPlus.BusinessObjectsNew.TurnAroundTimeRange.LoadList()

            Me.State.bnoRow = True
            dv = TurnAroundTimeRange.getEmptyList(dv)
            Me.Grid.DataSource = dv
            Me.Grid.DataBind()
            Me.Grid.Rows(0).Visible = False


        Else
            Me.State.bnoRow = False
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
            If (Me.State.AddingNewRow) Then
                Me.lblRecordCount.Text = (Me.State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            Else
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End If
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
    End Sub

    Private Sub PopulateBOFromForm()

        Try
            With Me.State.TurnAroundTimeRange


                .Code = CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.CODE_COL).FindControl(Me.CODE_CONTROL_NAME), TextBox).Text
                .Description = CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.CODE_COL).FindControl(Me.DESCRIPTION_CONTROL_NAME), TextBox).Text

                Dim minDaysString As String = CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.MIN_DAYS_COL).FindControl(Me.MIN_DAYS_CONTROL_NAME), TextBox).Text
                Try
                    .MinDays = CType(minDaysString, Long)
                   
                Catch ex As Exception
                    Throw New GUIException(Message.MSG_INVALID_MIN_MAX_VALUE, Assurant.ElitaPlus.Common.ErrorCodes.MIN_VALUE_MUST_BE_FROM_0_TO_9998)
                End Try

                Dim MaxDaysString As String = CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.MAX_DAYS_COL).FindControl(Me.MAX_DAYS_CONTROL_NAME), TextBox).Text
                Try
                    .MaxDays = CType(MaxDaysString, Long)
                Catch ex As Exception
                    Throw New GUIException(Message.MSG_INVALID_MIN_MAX_VALUE, Assurant.ElitaPlus.Common.ErrorCodes.MAX_VALUE_MUST_BE_FROM_1_TO_9999)
                End Try

                .ColorId = Me.GetSelectedItem(CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.COLOR_COL).FindControl(Me.COLOR_CONTROL_NAME), DropDownList))
                .CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

            End With
        Catch ex As Exception
            Throw ex 'Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub


    Private Sub PopulateFormFromBO()

        Dim gridRowIdx As Integer = Me.Grid.EditIndex
        Try
            With Me.State.TurnAroundTimeRange
                If Not .Description Is Nothing Then
                    CType(Me.Grid.Rows(gridRowIdx).Cells(Me.DESCRIPTION_COL).FindControl(Me.DESCRIPTION_CONTROL_NAME), TextBox).Text = .Description
                End If
                'If Not .Code Is Nothing Then
                '    CType(Me.Grid.Items(gridRowIdx).Cells(Me.CODE_COL).FindControl(Me.CODE_CONTROL_NAME), TextBox).Text = .Code
                'End If
                CType(Me.Grid.Rows(gridRowIdx).Cells(Me.ID_COL).FindControl(Me.ID_CONTROL_NAME), Label).Text = .Id.ToString
            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Private Sub ReturnFromEditing()

        Grid.EditIndex = NO_ROW_SELECTED_INDEX

        If Me.Grid.PageCount = 0 Then
            'if returning to the "1st time in" screen
            ControlMgr.SetVisibleControl(Me, Grid, False)
        Else
            ControlMgr.SetVisibleControl(Me, Grid, True)
        End If

        Me.State.IsEditMode = False
        Me.PopulateGrid()
        Me.State.PageIndex = Grid.PageIndex
        SetButtonsState()

    End Sub

    Private Sub SetButtonsState()

        If (Me.State.IsEditMode) Then
            ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, True)
            ControlMgr.SetVisibleControl(Me, CancelButton, True)
            ControlMgr.SetVisibleControl(Me, NewButton_WRITE, False)
            ControlMgr.SetEnableControl(Me, SearchButton, False)
            Me.MenuEnabled = False
            If (Me.cboPageSize.Visible) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, False)
            End If
        Else
            ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, False)
            ControlMgr.SetVisibleControl(Me, CancelButton, False)
            ControlMgr.SetVisibleControl(Me, NewButton_WRITE, True)
            ControlMgr.SetEnableControl(Me, SearchButton, True)
            Me.MenuEnabled = True
            If (Me.cboPageSize.Visible) Then
                ControlMgr.SetEnableControl(Me, cboPageSize, True)
            End If
        End If

    End Sub

    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
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

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging

        Try
            If (Not (Me.State.IsEditMode)) Then
                Me.State.PageIndex = e.NewPageIndex
                Me.Grid.PageIndex = Me.State.PageIndex
                Me.PopulateGrid()
                Me.Grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Protected Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

        Try
            Dim index As Integer

            If (e.CommandName = Me.EDIT_COMMAND) Then
                'Do the Edit here
                index = CInt(e.CommandArgument)
                'Set the IsEditMode flag to TRUE
                Me.State.IsEditMode = True

                Me.State.TurnAroundTimeRangeId = New Guid(CType(Me.Grid.Rows(index).Cells(Me.ID_COL).FindControl(Me.ID_CONTROL_NAME), Label).Text)

                Me.State.TurnAroundTimeRange = New Assurant.ElitaPlus.BusinessObjectsNew.TurnAroundTimeRange(Me.State.TurnAroundTimeRangeId)

                Me.PopulateGrid()

                Me.State.PageIndex = Grid.PageIndex

                'Disable all Edit and Delete icon buttons on the Grid
                SetGridControls(Me.Grid, False)

                'Set focus on the Description TextBox for the EditItemIndex row
                Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.DESCRIPTION_COL, Me.DESCRIPTION_CONTROL_NAME, index)

                Me.PopulateFormFromBO()

                Me.SetButtonsState()

            ElseIf (e.CommandName = Me.DELETE_COMMAND) Then
                'Do the delete here
                index = CInt(e.CommandArgument)
                'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                Grid.SelectedIndex = Me.NO_ROW_SELECTED_INDEX

                'Save the Id in the Session

                Me.State.TurnAroundTimeRangeId = New Guid(CType(Me.Grid.Rows(index).Cells(Me.ID_COL).FindControl(Me.ID_CONTROL_NAME), Label).Text)

                Me.State.TurnAroundTimeRange = New Assurant.ElitaPlus.BusinessObjectsNew.TurnAroundTimeRange(Me.State.TurnAroundTimeRangeId)
                Try
                    Me.State.TurnAroundTimeRange.Delete()
                    'Call the Save() method in the TurnAroundTimeRange Business Object here
                    Me.State.TurnAroundTimeRange.Save()
                Catch ex As Exception
                    Me.State.TurnAroundTimeRange.RejectChanges()
                    Throw ex
                End Try

                Me.State.PageIndex = Grid.PageIndex

                'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
                Me.State.IsAfterSave = True
                Me.State.searchDV = Nothing
                PopulateGrid()
                Me.State.PageIndex = Grid.PageIndex

            ElseIf ((e.CommandName = Me.SORT_COMMAND) AndAlso Not (Me.IsEditing)) Then


            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Protected Sub ItemBound(ByVal source As Object, ByVal e As GridViewRowEventArgs) Handles Grid.RowDataBound

        Try
            BaseItemBound(source, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub
    'The Binding Logic is here
    Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

        If Not dvRow Is Nothing And Not Me.State.bnoRow Then

            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                CType(e.Row.Cells(Me.ID_COL).FindControl(Me.ID_CONTROL_NAME), Label).Text = GetGuidStringFromByteArray(CType(dvRow("turn_around_time_range_id"), Byte()))


                If (Me.State.IsEditMode = True _
                        AndAlso Me.State.TurnAroundTimeRangeId.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow("turn_around_time_range_id"), Byte())))) Then
                    CType(e.Row.Cells(Me.CODE_COL).FindControl(Me.CODE_CONTROL_NAME), TextBox).Text = dvRow("Code").ToString
                    CType(e.Row.Cells(Me.DESCRIPTION_COL).FindControl(Me.DESCRIPTION_CONTROL_NAME), TextBox).Text = dvRow("DESCRIPTION").ToString
                    CType(e.Row.Cells(Me.MIN_DAYS_COL).FindControl(Me.MIN_DAYS_CONTROL_NAME), TextBox).Text = dvRow("Min_Days").ToString
                    CType(e.Row.Cells(Me.MAX_DAYS_COL).FindControl(Me.MAX_DAYS_CONTROL_NAME), TextBox).Text = dvRow("Max_Days").ToString

                    ' Me.BindListControlToDataView(CType(e.Row.Cells(Me.COLOR_COL).FindControl(Me.COLOR_CONTROL_NAME), DropDownList), LookupListNew.GetColorLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                    CType(e.Row.Cells(Me.COLOR_COL).FindControl(Me.COLOR_CONTROL_NAME), DropDownList).Populate(CommonConfigManager.Current.ListManager.GetList("COLORS", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                    {
                     .AddBlankItem = True
                   })
                    Me.SetSelectedItem(CType(e.Row.Cells(Me.COLOR_COL).FindControl(Me.COLOR_CONTROL_NAME), DropDownList), Me.State.TurnAroundTimeRange.ColorId)

                    If Me.State.AddingNewRow Then
                        CType(e.Row.Cells(Me.MIN_DAYS_COL).FindControl(Me.MIN_DAYS_CONTROL_NAME), TextBox).Enabled = True
                        CType(e.Row.Cells(Me.MAX_DAYS_COL).FindControl(Me.MAX_DAYS_CONTROL_NAME), TextBox).Enabled = True
                    ElseIf e.Row.RowIndex = 0 Then 'allow only the min value on the first record and the max value value of the last record to be editied.
                        CType(e.Row.Cells(Me.MIN_DAYS_COL).FindControl(Me.MIN_DAYS_CONTROL_NAME), TextBox).Enabled = True
                    ElseIf e.Row.RowIndex = Me.State.searchDV.Count - 1 Then
                        CType(e.Row.Cells(Me.MAX_DAYS_COL).FindControl(Me.MAX_DAYS_CONTROL_NAME), TextBox).Enabled = True
                    End If

                Else
                    CType(e.Row.Cells(Me.CODE_COL).FindControl("CodeLabel"), Label).Text = dvRow("Code").ToString
                    CType(e.Row.Cells(Me.DESCRIPTION_COL).FindControl("DescriptionLabel"), Label).Text = dvRow("DESCRIPTION").ToString
                    CType(e.Row.Cells(Me.MIN_DAYS_COL).FindControl("MinDaysLabel"), Label).Text = dvRow("Min_Days").ToString
                    CType(e.Row.Cells(Me.MAX_DAYS_COL).FindControl("MaxDaysLabel"), Label).Text = dvRow("Max_days").ToString
                    CType(e.Row.Cells(Me.COLOR_COL).FindControl("ColorLabel"), Label).Text = dvRow("color").ToString
                End If
            End If
        End If
    End Sub

    Protected Sub ItemCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        BaseItemCreated(sender, e)


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
            Me.State.SortExpression = Me.SortDirection
            Me.State.PageIndex = 0
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Protected Sub BindBoPropertiesToGridHeaders()
        Me.BindBOPropertyToGridHeader(Me.State.TurnAroundTimeRange, "Code", Me.Grid.Columns(Me.CODE_COL))
        Me.BindBOPropertyToGridHeader(Me.State.TurnAroundTimeRange, "Description", Me.Grid.Columns(Me.DESCRIPTION_COL))
        Me.BindBOPropertyToGridHeader(Me.State.TurnAroundTimeRange, "MinDays", Me.Grid.Columns(Me.MIN_DAYS_COL))
        Me.BindBOPropertyToGridHeader(Me.State.TurnAroundTimeRange, "MaxDays", Me.Grid.Columns(Me.MAX_DAYS_COL))
        Me.BindBOPropertyToGridHeader(Me.State.TurnAroundTimeRange, "ColorId", Me.Grid.Columns(Me.COLOR_COL))

        Me.ClearGridViewHeadersAndLabelsErrSign()
    End Sub

    Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As GridView, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)
        'Set focus on the Description TextBox for the EditItemIndex row
        Dim desc As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
        SetFocus(desc)
    End Sub

#End Region

End Class
