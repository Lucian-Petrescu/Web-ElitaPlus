Option Strict On
Option Explicit On

Namespace Tables

    Partial Class EarningPatternCodeForm
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
                IsEditing = (Me.Grid.EditItemIndex > NO_ROW_SELECTED_INDEX)
            End Get
        End Property

#End Region

#Region "Page State"
        Class MyState
            Public PageIndex As Integer = 0
            'Public MyBO As EarningCode = New EarningCode
            Public MyBO As EarningCode
            Public DescriptionMask As String
            Public CodeMask As String
            Public CompanyGroupId As Guid
            Public Id As Guid
            Public IsAfterSave As Boolean
            Public IsEditMode As Boolean
            Public IsGridVisible As Boolean
            Public searchDV As DataView = Nothing
            Public SortExpression As String = EarningCode.EarningCodeSearchDV.COL_CODE
            Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
            Public AddingNewRow As Boolean
            Public Canceling As Boolean
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

        '    Protected WithEvents lblPageSize As System.Web.UI.WebControls.Label
        '    Protected WithEvents cboPageSize As System.Web.UI.WebControls.DropDownList
        '    Protected WithEvents lblRecordCount As System.Web.UI.WebControls.Label
        '    Protected WithEvents trPageSize As System.Web.UI.HtmlControls.HtmlTableRow


        Protected WithEvents ErrController As ErrorController
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label
        Protected WithEvents Label2 As System.Web.UI.WebControls.Label

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

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

        Private Const ID_COL_IDX As Integer = 2
        Private Const CODE_COL_IDX As Integer = 3
        Private Const DESCRIPTION_COL_IDX As Integer = 4

        Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

        Private Const DESCRIPTION_CONTROL_NAME As String = "DescriptionTextBox"
        Private Const CODE_CONTROL_NAME As String = "CodeTextBox"
        Private Const ID_CONTROL_NAME As String = "IdLabel"

        Private Const EDIT_COMMAND As String = "EditRecord"
        Private Const DELETE_COMMAND As String = "DeleteRecord"
        Private Const SORT_COMMAND As String = "Sort"

        Private Const NO_ROW_SELECTED_INDEX As Integer = -1

#End Region

#Region "Button Click Handlers"

        Private Sub SearchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchButton.Click
            Try
                Me.State.PageIndex = 0
                Me.State.Id = Guid.Empty
                Me.State.IsGridVisible = True
                Me.State.searchDV = Nothing
                PopulateGrid()
                Me.State.PageIndex = Grid.CurrentPageIndex
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try

        End Sub

        Private Sub ClearButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearButton.Click

            Try
                ClearSearchCriteria()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Private Sub ClearSearchCriteria()

            Try
                SearchDescriptionTextBox.Text = String.Empty
                SearchCodeTextBox.Text = String.Empty

                'Me.Grid.CurrentPageIndex = 0
                'Me.Grid.DataSource = Nothing
                'Me.Grid.DataBind()
                'ControlMgr.SetVisibleControl(Me, trPageSize, False)
                'Me.State.Id = Guid.Empty

                'Update Page State
                With Me.State
                    .DescriptionMask = SearchDescriptionTextBox.Text
                    .CodeMask = SearchCodeTextBox.Text
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try

        End Sub

        Private Sub NewButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewButton_WRITE.Click

            Try
                Me.State.IsEditMode = True
                Me.State.IsGridVisible = True
                Me.State.AddingNewRow = True
                AddNew()
                Me.SetGridControls(Me.Grid, False)
                SetButtonsState()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try

        End Sub

        Private Sub SaveButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton_WRITE.Click

            Try
                PopulateBOFromForm()
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
                Me.SetStateProperties()
                If Not Page.IsPostBack Then
                    Me.SetDefaultButton(Me.SearchCodeTextBox, Me.SearchButton)
                    Me.SetDefaultButton(Me.SearchDescriptionTextBox, Me.SearchButton)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Me.SetGridItemStyleColor(Me.Grid)
                    Me.State.PageIndex = 0
                    If Me.State.MyBO Is Nothing Then
                        Me.State.MyBO = New EarningCode
                    End If
                    SetButtonsState()
                End If
                BindBoPropertiesToGridHeaders()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
            Me.ShowMissingTranslations(ErrController)
        End Sub

        Private Sub PopulateGrid()

            Try
                'Refresh the DataView and Call SetPageAndSelectedIndexFromGuid() to go to the Page 
                'where the most recently saved Record exists in the DataView

                If (Me.State.searchDV Is Nothing) Then
                    Me.State.searchDV = GetGridDataView()
                    '  Me.State.searchDV.Sort = Me.Grid.DataMember
                End If
                'Ticket # 748479 - Search grids in Tables tab should not show pop-up message when number of retrieved record is over 1,000
                'Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
                Me.State.searchDV.Sort = Me.State.SortExpression

                If (Me.State.IsAfterSave) Then
                    Me.State.IsAfterSave = False
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.Grid, Me.State.PageIndex)
                ElseIf (Me.State.IsEditMode) Then
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
                Else
                    'In a Delete scenario...
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
                End If
                Me.Grid.AutoGenerateColumns = False
                Me.Grid.Columns(Me.DESCRIPTION_COL_IDX).SortExpression = EarningCode.EarningCodeSearchDV.COL_DESCRIPTION
                Me.Grid.Columns(Me.CODE_COL_IDX).SortExpression = EarningCode.EarningCodeSearchDV.COL_CODE

                SortAndBindGrid()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try

        End Sub

        Private Function GetGridDataView() As DataView

            With State
                Return (EarningCode.LoadList(.DescriptionMask, .CodeMask, .CompanyGroupId))
            End With

        End Function

        Private Sub SetStateProperties()

            Me.State.DescriptionMask = SearchDescriptionTextBox.Text
            Me.State.CodeMask = SearchCodeTextBox.Text
            Me.State.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

        End Sub

        Private Sub SortAndBindGrid()
            Me.State.PageIndex = Me.Grid.CurrentPageIndex
            Me.Grid.DataSource = Me.State.searchDV
            HighLightSortColumn(Grid, Me.State.SortExpression)
            Me.Grid.DataBind()

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


        Private Sub AddNew()
            Me.State.searchDV = GetGridDataView()

            Me.State.MyBO = New EarningCode
            Me.State.Id = Me.State.MyBO.Id

            Me.State.searchDV = Me.State.MyBO.GetNewDataViewRow(Me.State.searchDV, Me.State.Id, Me.State.MyBO)

            Grid.DataSource = Me.State.searchDV

            SetGridControls(Me.Grid, False)

            Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)

            Me.Grid.AutoGenerateColumns = False
            Me.Grid.Columns(Me.DESCRIPTION_COL_IDX).SortExpression = EarningCode.EarningCodeSearchDV.COL_DESCRIPTION
            Me.Grid.Columns(Me.CODE_COL_IDX).SortExpression = EarningCode.EarningCodeSearchDV.COL_CODE

            SortAndBindGrid()

            'Set focus on the Code TextBox for the EditItemIndex row
            Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.CODE_COL_IDX, Me.CODE_CONTROL_NAME, Me.Grid.EditItemIndex)

        End Sub

        Private Sub PopulateBOFromForm()

            Try
                With Me.State.MyBO
                    .Description = CType(Me.Grid.Items(Me.Grid.EditItemIndex).Cells(Me.DESCRIPTION_COL_IDX).FindControl(Me.DESCRIPTION_CONTROL_NAME), TextBox).Text
                    .Code = CType(Me.Grid.Items(Me.Grid.EditItemIndex).Cells(Me.CODE_COL_IDX).FindControl(Me.CODE_CONTROL_NAME), TextBox).Text
                    .CompanyGroupId = Me.State.CompanyGroupId
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try

        End Sub

        Private Sub PopulateFormFromBO()

            Dim gridRowIdx As Integer = Me.Grid.EditItemIndex
            Try
                With Me.State.MyBO
                    If Not .Description Is Nothing Then
                        CType(Me.Grid.Items(gridRowIdx).Cells(Me.DESCRIPTION_COL_IDX).FindControl(Me.DESCRIPTION_CONTROL_NAME), TextBox).Text = .Description
                    End If
                    If Not .Code Is Nothing Then
                        CType(Me.Grid.Items(gridRowIdx).Cells(Me.CODE_COL_IDX).FindControl(Me.CODE_CONTROL_NAME), TextBox).Text = .Code
                    End If
                    'If Not (.Id.Equals(Guid.Empty)) Then
                    '    CType(Me.Grid.Items(gridRowIdx).Cells(Me.ID_COL_IDX).FindControl(Me.ID_CONTROL_NAME), Label).Text = .Id.ToString
                    'End If
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
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

        Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged

            Try
                If (Not (Me.State.IsEditMode)) Then
                    Me.State.PageIndex = e.NewPageIndex
                    Me.Grid.CurrentPageIndex = Me.State.PageIndex
                    Me.PopulateGrid()
                    Me.Grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try

        End Sub

        Protected Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)

            Try
                Dim index As Integer = e.Item.ItemIndex

                If (e.CommandName = Me.EDIT_COMMAND) Then
                    'Do the Edit here

                    'Set the IsEditMode flag to TRUE
                    Me.State.IsEditMode = True

                    Me.State.Id = New Guid(CType(Me.Grid.Items(e.Item.ItemIndex).Cells(Me.ID_COL_IDX).FindControl(Me.ID_CONTROL_NAME), Label).Text)

                    Me.State.MyBO = New EarningCode(Me.State.Id)

                    Me.PopulateGrid()

                    Me.State.PageIndex = Grid.CurrentPageIndex

                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(Me.Grid, False)

                    'Set focus on the Description TextBox for the EditItemIndex row
                    Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.CODE_COL_IDX, Me.CODE_CONTROL_NAME, index)

                    Me.PopulateFormFromBO()

                    Me.SetButtonsState()

                ElseIf (e.CommandName = Me.DELETE_COMMAND) Then
                    'Do the delete here

                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    Grid.SelectedIndex = Me.NO_ROW_SELECTED_INDEX

                    'Save the Id in the Session

                    Me.State.Id = New Guid(CType(Me.Grid.Items(e.Item.ItemIndex).Cells(Me.ID_COL_IDX).FindControl(Me.ID_CONTROL_NAME), Label).Text)

                    Me.State.MyBO = New EarningCode(Me.State.Id)
                    Try
                        Me.State.MyBO.Delete()
                        'Call the Save() method in the EarningCode Business Object here
                        Me.State.MyBO.Save()
                    Catch ex As Exception
                        Me.State.MyBO.RejectChanges()
                        Throw ex
                    End Try

                    Me.State.PageIndex = Grid.CurrentPageIndex

                    'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
                    Me.State.IsAfterSave = True
                    Me.State.searchDV = Nothing
                    PopulateGrid()
                    Me.State.PageIndex = Grid.CurrentPageIndex

                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try

        End Sub

        Protected Sub ItemBound(ByVal source As Object, ByVal e As DataGridItemEventArgs) Handles Grid.ItemDataBound

            Try
                BaseItemBound(source, e)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Protected Sub ItemCreated(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles Grid.SortCommand

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
                'Set the Me.State.selectedClaimId = Guid.Empty and set Me.State.PageIndex = 0
                Me.State.Id = Guid.Empty
                Me.State.PageIndex = 0

                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try

        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "Description", Me.Grid.Columns(Me.DESCRIPTION_COL_IDX))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "Code", Me.Grid.Columns(Me.CODE_COL_IDX))
            Me.ClearGridHeadersAndLabelsErrSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As DataGrid, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim desc As TextBox = CType(grid.Items(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(desc)
        End Sub

#End Region

    End Class

End Namespace

