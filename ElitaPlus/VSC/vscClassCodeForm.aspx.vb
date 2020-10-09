Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace VSC

    Partial Class vscClassCodeForm
        Inherits ElitaPlusSearchPage


#Region "Member Variables"

        Private Shared pageIndex As Integer
        Private Shared pageCount As Integer

#End Region


#Region "Properties"

        Public ReadOnly Property IsEditing() As Boolean
            Get
                IsEditing = (moDataGrid.EditItemIndex > NO_ROW_SELECTED_INDEX)
            End Get
        End Property

#End Region

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Protected WithEvents moErrorController As ErrorController
        'Protected WithEvents SaveButton_WRITE As System.Web.UI.WebControls.ImageButton
        'Protected WithEvents CancelButton As System.Web.UI.WebControls.ImageButton


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

        Public Const GRID_COL_EDIT As Integer = 0
        Public Const GRID_COL_CLASS_CODE_ID As Integer = 2
        Public Const GRID_COL_CLASS_CODE As Integer = 3
        Public Const GRID_COL_ACTIVE As Integer = 4

        Private Const EDIT_COMMAND As String = "EditRecord"
        Private Const DELETE_COMMAND As String = "DeleteRecord"
        Private Const SORT_COMMAND As String = "Sort"

        Private Const GRID_NAME_CLASS_CODE As String = "moClassCodeGrid"
        Private Const GRID_NAME_ACTIVE As String = "moActiveDwGrid"


        Private Const NO_ROW_SELECTED_INDEX As Integer = -1
        Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

#End Region

#Region "Page State"
        Class MyState

            Public PageIndex As Integer = 0
            '  Public MyBO As VSCClassCode = New VSCClassCode
            Public MyBO As VSCClassCode
            Public SearchClassCodeId As Guid = Guid.Empty

            Public SortExpression As String = VSCClassCode.ClassCodeSearchDV.COL_NAME_CODE

            Public ClassCodeId As Guid = Guid.Empty
            Public LangId As Guid
            Public CompanyId As Guid

            Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE

            Public IsGridVisible As Boolean
            Public IsAfterSave As Boolean
            Public IsEditMode As Boolean
            Public AddingNewRow As Boolean
            Public Canceling As Boolean

            Public searchDV As DataView = Nothing
            Public HasDataChanged As Boolean


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

#Region "Page_Events"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

            Try
                moErrorController.Clear_Hide()
                If Not Page.IsPostBack Then
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    SetGridItemStyleColor(moDataGrid)
                    If State.MyBO Is Nothing Then
                        State.MyBO = New VSCClassCode
                    End If
                    PopulateDropdowns()
                    State.PageIndex = 0
                    SetButtonsState()
                End If
                BindBoPropertiesToGridHeaders()
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
            ShowMissingTranslations(moErrorController)

        End Sub

        Private Sub ReturnFromEditing()

            moDataGrid.EditItemIndex = NO_ROW_SELECTED_INDEX

            If moDataGrid.PageCount = 0 Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, moDataGrid, False)
            Else
                ControlMgr.SetVisibleControl(Me, moDataGrid, True)
            End If
            SetGridControls(moDataGrid, True)
            State.IsEditMode = False
            PopulateDropdowns()
            PopulateGrid()
            State.PageIndex = moDataGrid.CurrentPageIndex
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

#Region "Populate Handlers"


        Private Sub PopulateDropdowns()

            Try

                State.CompanyId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                ' Me.BindListControlToDataView(Me.moClassCodeDrop, LookupListNew.GetVSCClassCodesLookupList(Me.State.CompanyId)) 'VscClassCodesByCompanyGroup
                Dim listcontext As ListContext = New ListContext()
                listcontext.CompanyGroupId = State.CompanyId
                Dim vsccodeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.VscClassCodesByCompanyGroup, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                moClassCodeDrop.Populate(vsccodeLkl, New PopulateOptions() With
                    {
                    .AddBlankItem = True
                    })
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try

        End Sub
        Public Sub PopulateGrid()

            Try
                If (State.searchDV Is Nothing) Then
                    State.searchDV = GetGridDataView()
                End If

                moDataGrid.AutoGenerateColumns = False
                If (State.IsAfterSave) Then
                    State.IsAfterSave = False
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.ClassCodeId, moDataGrid, State.PageIndex)
                ElseIf (State.IsEditMode) Then
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.ClassCodeId, moDataGrid, State.PageIndex, State.IsEditMode)
                Else
                    SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, moDataGrid, State.PageIndex)
                End If

                State.searchDV.Sort = State.SortExpression
                moDataGrid.Columns(GRID_COL_CLASS_CODE).SortExpression = VSCClassCode.ClassCodeSearchDV.COL_NAME_CODE
                moDataGrid.Columns(GRID_COL_ACTIVE).SortExpression = VSCClassCode.ClassCodeSearchDV.COL_NAME_DESCRIPTION
                SortAndBindGrid()

            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try

        End Sub
        Private Sub AddNew()

            State.searchDV = GetGridDataView()

            State.MyBO = New VSCClassCode
            State.ClassCodeId = State.MyBO.Id

            State.searchDV = State.MyBO.GetNewDataViewRow(State.searchDV, State.ClassCodeId)

            moDataGrid.DataSource = State.searchDV
            SetGridControls(moDataGrid, False)
            SetPageAndSelectedIndexFromGuid(State.searchDV, State.ClassCodeId, moDataGrid, State.PageIndex, State.IsEditMode)

            moDataGrid.AutoGenerateColumns = False
            moDataGrid.Columns(GRID_COL_CLASS_CODE).SortExpression = VSCClassCode.ClassCodeSearchDV.COL_NAME_CODE
            moDataGrid.Columns(GRID_COL_ACTIVE).SortExpression = VSCClassCode.ClassCodeSearchDV.COL_NAME_DESCRIPTION

            SortAndBindGrid()

            'Set focus on the Dealer dropdown list for the EditItemIndex row
            SetFocusOnEditableFieldInGrid(moDataGrid, GRID_COL_CLASS_CODE, moDataGrid.EditItemIndex)

        End Sub

        Private Function GetGridDataView() As DataView

            With State
                Return (VSCClassCode.getList(GetSelectedItem(moClassCodeDrop), Guid.Empty, State.CompanyId))
            End With

        End Function
        Private Sub SortAndBindGrid()

            TranslateGridControls(moDataGrid)
            State.PageIndex = moDataGrid.CurrentPageIndex
            moDataGrid.DataSource = State.searchDV
            HighLightSortColumn(moDataGrid, State.SortExpression)
            moDataGrid.DataBind()

            ControlMgr.SetVisibleControl(Me, moDataGrid, State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, moDataGrid.Visible)

            Session("recCount") = State.searchDV.Count

            If moDataGrid.Visible Then
                If (State.AddingNewRow) Then
                    lblRecordCount.Text = (State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, moDataGrid)

        End Sub

        Private Sub PopulateBOFromForm()

            With State.MyBO
                .Code = CType(moDataGrid.Items(moDataGrid.EditItemIndex).Cells(GRID_COL_CLASS_CODE).FindControl(GRID_NAME_CLASS_CODE), TextBox).Text
                '       Me.PopulateBOProperty(Me.State.MyBO, "code", CType(Me.GetSelectedGridControl(moDataGrid, GRID_COL_CLASS_CODE), TextBox))
                .Active = GetSelectedItem(CType(moDataGrid.Items(moDataGrid.EditItemIndex).Cells(GRID_COL_ACTIVE).FindControl(GRID_NAME_ACTIVE), DropDownList))
                PopulateBOProperty(State.MyBO, "CompanyGroup", ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            End With
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If

        End Sub


#End Region

#Region "Button Click Handlers"

        Private Sub ClearButton_Click(sender As System.Object, e As System.EventArgs) Handles ClearButton.Click

            Try

                moClassCodeDrop.SelectedIndex = 0
                moDataGrid.CurrentPageIndex = NO_PAGE_INDEX
                moDataGrid.DataSource = Nothing
                moDataGrid.DataBind()
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                State.ClassCodeId = Guid.Empty

            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub SearchButton_Click(sender As System.Object, e As System.EventArgs) Handles SearchButton.Click

            Try
                State.PageIndex = 0
                State.ClassCodeId = Guid.Empty
                State.IsGridVisible = True
                State.searchDV = Nothing
                PopulateGrid()
                State.PageIndex = moDataGrid.CurrentPageIndex
            Catch ex As Exception
                HandleErrors(ex, moErrorController)

            End Try

        End Sub
        Private Sub CancelButton_Click(sender As System.Object, e As System.EventArgs) Handles CancelButton.Click

            Try
                moDataGrid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                State.Canceling = True
                If (State.AddingNewRow) Then
                    State.AddingNewRow = False
                    State.searchDV = Nothing
                End If
                ReturnFromEditing()
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try

        End Sub

        Private Sub NewButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles NewButton_WRITE.Click

            Try
                State.IsEditMode = True
                State.IsGridVisible = True
                State.AddingNewRow = True
                AddNew()
                SetGridControls(moDataGrid, False)
                SetButtonsState()
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
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
                    State.searchDV = Nothing
                    ReturnFromEditing()
                Else
                    AddInfoMsg(MSG_RECORD_NOT_SAVED)
                    ReturnFromEditing()
                End If
            Catch ex As Exception

                HandleErrors(ex, moErrorController)

            End Try

        End Sub


#End Region


#Region " Datagrid Related "
        Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles moDataGrid.ItemDataBound
            Try
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

                If (itemType = ListItemType.Item OrElse
                    itemType = ListItemType.AlternatingItem OrElse
                    itemType = ListItemType.SelectedItem) Then

                    e.Item.Cells(GRID_COL_CLASS_CODE_ID).Text = GetGuidStringFromByteArray(CType(dvRow(VSCClassCode.ClassCodeSearchDV.COL_NAME_VSC_CLASS_CODE_ID), Byte()))
                    e.Item.Cells(GRID_COL_CLASS_CODE).Text = dvRow(VSCClassCode.ClassCodeSearchDV.COL_NAME_CODE).ToString

                    e.Item.Cells(GRID_COL_ACTIVE).Text = GetGuidStringFromByteArray(CType(dvRow(VSCClassCode.ClassCodeSearchDV.COL_NAME_ACTIVE), Byte()))
                    e.Item.Cells(GRID_COL_ACTIVE).Text = dvRow(VSCClassCode.ClassCodeSearchDV.COL_NAME_DESCRIPTION).ToString

                ElseIf (itemType = ListItemType.EditItem) Then

                    e.Item.Cells(GRID_COL_CLASS_CODE_ID).Text = GetGuidStringFromByteArray(CType(dvRow(VSCClassCode.ClassCodeSearchDV.COL_NAME_VSC_CLASS_CODE_ID), Byte()))
                    CType(e.Item.Cells(GRID_COL_CLASS_CODE).FindControl(GRID_NAME_CLASS_CODE), TextBox).Text = dvRow(VSCClassCode.ClassCodeSearchDV.COL_NAME_CODE).ToString

                    Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

                    'Me.BindListControlToDataView(CType(e.Item.Cells(Me.GRID_COL_ACTIVE).FindControl(GRID_NAME_ACTIVE), DropDownList), LookupListNew.DropdownLookupList("YESNO", langId, True))
                    CType(e.Item.Cells(GRID_COL_ACTIVE).FindControl(GRID_NAME_ACTIVE), DropDownList).Populate(CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                           {
                                            .AddBlankItem = True
                                           })
                    SetSelectedItem(CType(e.Item.Cells(GRID_COL_ACTIVE).FindControl(GRID_NAME_ACTIVE), DropDownList), State.MyBO.Active)

                End If
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try

        End Sub
        Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moDataGrid.PageIndexChanged

            Try
                If (Not (State.IsEditMode)) Then
                    State.PageIndex = e.NewPageIndex
                    moDataGrid.CurrentPageIndex = State.PageIndex
                    PopulateGrid()
                    moDataGrid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try

        End Sub

        Protected Sub ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)

            Try
                Dim index As Integer = e.Item.ItemIndex

                If (e.CommandName = EDIT_COMMAND) Then
                    'Do the Edit here

                    'Set the IsEditMode flag to TRUE
                    State.IsEditMode = True

                    State.ClassCodeId = New Guid(moDataGrid.Items(e.Item.ItemIndex).Cells(GRID_COL_CLASS_CODE_ID).Text)

                    State.MyBO = New VSCClassCode(State.ClassCodeId)

                    PopulateGrid()

                    State.PageIndex = moDataGrid.CurrentPageIndex

                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(moDataGrid, False)

                    'Set focus on the Dealer dropdown list for the EditItemIndex row
                    SetFocusOnEditableFieldInGrid(moDataGrid, GRID_COL_CLASS_CODE, index)

                    SetButtonsState()

                ElseIf (e.CommandName = DELETE_COMMAND) Then
                    'Do the delete here

                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    moDataGrid.SelectedIndex = NO_ROW_SELECTED_INDEX

                    'Save the Id in the Session
                    State.ClassCodeId = New Guid(moDataGrid.Items(e.Item.ItemIndex).Cells(GRID_COL_CLASS_CODE_ID).Text)
                    State.MyBO = New VSCClassCode(State.ClassCodeId)

                    Try
                        State.MyBO.Delete()
                        'Call the Save() method in the Business Object here
                        State.MyBO.Save()
                    Catch ex As Exception
                        State.MyBO.RejectChanges()
                        Throw ex
                    End Try

                    State.PageIndex = moDataGrid.CurrentPageIndex

                    'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
                    State.IsAfterSave = True

                    State.searchDV = Nothing
                    PopulateDropdowns()
                    PopulateGrid()
                    State.PageIndex = moDataGrid.CurrentPageIndex
                End If

            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try

        End Sub

        Protected Sub ItemBound(source As Object, e As DataGridItemEventArgs) Handles moDataGrid.ItemDataBound
            BaseItemBound(source, e)
        End Sub

        Protected Sub ItemCreated(sender As Object, e As DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                moDataGrid.CurrentPageIndex = NewCurrentPageIndex(moDataGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles moDataGrid.SortCommand

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
                HandleErrors(ex, moErrorController)
            End Try
        End Sub


        Protected Sub BindBoPropertiesToGridHeaders()

            BindBOPropertyToGridHeader(State.MyBO, "active", moDataGrid.Columns(GRID_COL_ACTIVE))
            BindBOPropertyToGridHeader(State.MyBO, "ClassCode", moDataGrid.Columns(GRID_COL_CLASS_CODE))

            ClearGridHeadersAndLabelsErrSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(grid As DataGrid, cellPosition As Integer, itemIndex As Integer)

            ''Set focus on the specified control on the EditItemIndex row for the grid

            Dim ctrlActive As DropDownList = CType(grid.Items(itemIndex).Cells(GRID_COL_ACTIVE).FindControl(GRID_NAME_ACTIVE), DropDownList)
            SetSelectedItem(ctrlActive, State.MyBO.Active)

            Dim ctrlCode As TextBox = CType(grid.Items(itemIndex).Cells(GRID_COL_CLASS_CODE).FindControl(GRID_NAME_CLASS_CODE), TextBox)
            SetFocus(ctrlCode)




        End Sub


#End Region
    End Class
End Namespace