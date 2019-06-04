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
                IsEditing = (Me.moDataGrid.EditItemIndex > NO_ROW_SELECTED_INDEX)
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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Try
                moErrorController.Clear_Hide()
                If Not Page.IsPostBack Then
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Me.SetGridItemStyleColor(Me.moDataGrid)
                    If Me.State.MyBO Is Nothing Then
                        Me.State.MyBO = New VSCClassCode
                    End If
                    Me.PopulateDropdowns()
                    Me.State.PageIndex = 0
                    SetButtonsState()
                End If
                BindBoPropertiesToGridHeaders()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try
            Me.ShowMissingTranslations(moErrorController)

        End Sub

        Private Sub ReturnFromEditing()

            moDataGrid.EditItemIndex = NO_ROW_SELECTED_INDEX

            If Me.moDataGrid.PageCount = 0 Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, moDataGrid, False)
            Else
                ControlMgr.SetVisibleControl(Me, moDataGrid, True)
            End If
            SetGridControls(moDataGrid, True)
            Me.State.IsEditMode = False
            Me.PopulateDropdowns()
            Me.PopulateGrid()
            Me.State.PageIndex = moDataGrid.CurrentPageIndex
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


#End Region

#Region "Populate Handlers"


        Private Sub PopulateDropdowns()

            Try

                Me.State.CompanyId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                ' Me.BindListControlToDataView(Me.moClassCodeDrop, LookupListNew.GetVSCClassCodesLookupList(Me.State.CompanyId)) 'VscClassCodesByCompanyGroup
                Dim listcontext As ListContext = New ListContext()
                listcontext.CompanyGroupId = Me.State.CompanyId
                Dim vsccodeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.VscClassCodesByCompanyGroup, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                Me.moClassCodeDrop.Populate(vsccodeLkl, New PopulateOptions() With
                    {
                    .AddBlankItem = True
                    })
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try

        End Sub
        Public Sub PopulateGrid()

            Try
                If (Me.State.searchDV Is Nothing) Then
                    Me.State.searchDV = GetGridDataView()
                End If

                Me.moDataGrid.AutoGenerateColumns = False
                If (Me.State.IsAfterSave) Then
                    Me.State.IsAfterSave = False
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.ClassCodeId, Me.moDataGrid, Me.State.PageIndex)
                ElseIf (Me.State.IsEditMode) Then
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.ClassCodeId, Me.moDataGrid, Me.State.PageIndex, Me.State.IsEditMode)
                Else
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.moDataGrid, Me.State.PageIndex)
                End If

                Me.State.searchDV.Sort = Me.State.SortExpression
                Me.moDataGrid.Columns(Me.GRID_COL_CLASS_CODE).SortExpression = VSCClassCode.ClassCodeSearchDV.COL_NAME_CODE
                Me.moDataGrid.Columns(Me.GRID_COL_ACTIVE).SortExpression = VSCClassCode.ClassCodeSearchDV.COL_NAME_DESCRIPTION
                Me.SortAndBindGrid()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try

        End Sub
        Private Sub AddNew()

            Me.State.searchDV = GetGridDataView()

            Me.State.MyBO = New VSCClassCode
            Me.State.ClassCodeId = Me.State.MyBO.Id

            Me.State.searchDV = Me.State.MyBO.GetNewDataViewRow(Me.State.searchDV, Me.State.ClassCodeId)

            moDataGrid.DataSource = Me.State.searchDV
            SetGridControls(Me.moDataGrid, False)
            Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.ClassCodeId, Me.moDataGrid, Me.State.PageIndex, Me.State.IsEditMode)

            Me.moDataGrid.AutoGenerateColumns = False
            Me.moDataGrid.Columns(Me.GRID_COL_CLASS_CODE).SortExpression = VSCClassCode.ClassCodeSearchDV.COL_NAME_CODE
            Me.moDataGrid.Columns(Me.GRID_COL_ACTIVE).SortExpression = VSCClassCode.ClassCodeSearchDV.COL_NAME_DESCRIPTION

            Me.SortAndBindGrid()

            'Set focus on the Dealer dropdown list for the EditItemIndex row
            Me.SetFocusOnEditableFieldInGrid(Me.moDataGrid, Me.GRID_COL_CLASS_CODE, Me.moDataGrid.EditItemIndex)

        End Sub

        Private Function GetGridDataView() As DataView

            With State
                Return (VSCClassCode.getList(Me.GetSelectedItem(moClassCodeDrop), Guid.Empty, Me.State.CompanyId))
            End With

        End Function
        Private Sub SortAndBindGrid()

            Me.TranslateGridControls(moDataGrid)
            Me.State.PageIndex = Me.moDataGrid.CurrentPageIndex
            Me.moDataGrid.DataSource = Me.State.searchDV
            HighLightSortColumn(moDataGrid, Me.State.SortExpression)
            Me.moDataGrid.DataBind()

            ControlMgr.SetVisibleControl(Me, moDataGrid, Me.State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Me.moDataGrid.Visible)

            Session("recCount") = Me.State.searchDV.Count

            If Me.moDataGrid.Visible Then
                If (Me.State.AddingNewRow) Then
                    Me.lblRecordCount.Text = (Me.State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, moDataGrid)

        End Sub

        Private Sub PopulateBOFromForm()

            With Me.State.MyBO
                .Code = CType(Me.moDataGrid.Items(Me.moDataGrid.EditItemIndex).Cells(Me.GRID_COL_CLASS_CODE).FindControl(GRID_NAME_CLASS_CODE), TextBox).Text
                '       Me.PopulateBOProperty(Me.State.MyBO, "code", CType(Me.GetSelectedGridControl(moDataGrid, GRID_COL_CLASS_CODE), TextBox))
                .Active = Me.GetSelectedItem(CType(moDataGrid.Items(moDataGrid.EditItemIndex).Cells(Me.GRID_COL_ACTIVE).FindControl(GRID_NAME_ACTIVE), DropDownList))
                Me.PopulateBOProperty(Me.State.MyBO, "CompanyGroup", ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            End With
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If

        End Sub


#End Region

#Region "Button Click Handlers"

        Private Sub ClearButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearButton.Click

            Try

                Me.moClassCodeDrop.SelectedIndex = 0
                moDataGrid.CurrentPageIndex = Me.NO_PAGE_INDEX
                moDataGrid.DataSource = Nothing
                moDataGrid.DataBind()
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                Me.State.ClassCodeId = Guid.Empty

            Catch ex As Exception
                Me.HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub SearchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchButton.Click

            Try
                Me.State.PageIndex = 0
                Me.State.ClassCodeId = Guid.Empty
                Me.State.IsGridVisible = True
                Me.State.searchDV = Nothing
                Me.PopulateGrid()
                Me.State.PageIndex = moDataGrid.CurrentPageIndex
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)

            End Try

        End Sub
        Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click

            Try
                Me.moDataGrid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                Me.State.Canceling = True
                If (Me.State.AddingNewRow) Then
                    Me.State.AddingNewRow = False
                    Me.State.searchDV = Nothing
                End If
                ReturnFromEditing()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try

        End Sub

        Private Sub NewButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewButton_WRITE.Click

            Try
                Me.State.IsEditMode = True
                Me.State.IsGridVisible = True
                Me.State.AddingNewRow = True
                AddNew()
                SetGridControls(Me.moDataGrid, False)
                SetButtonsState()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
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

                Me.HandleErrors(ex, Me.moErrorController)

            End Try

        End Sub


#End Region


#Region " Datagrid Related "
        Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles moDataGrid.ItemDataBound
            Try
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

                If (itemType = ListItemType.Item OrElse
                    itemType = ListItemType.AlternatingItem OrElse
                    itemType = ListItemType.SelectedItem) Then

                    e.Item.Cells(Me.GRID_COL_CLASS_CODE_ID).Text = GetGuidStringFromByteArray(CType(dvRow(VSCClassCode.ClassCodeSearchDV.COL_NAME_VSC_CLASS_CODE_ID), Byte()))
                    e.Item.Cells(Me.GRID_COL_CLASS_CODE).Text = dvRow(VSCClassCode.ClassCodeSearchDV.COL_NAME_CODE).ToString

                    e.Item.Cells(Me.GRID_COL_ACTIVE).Text = GetGuidStringFromByteArray(CType(dvRow(VSCClassCode.ClassCodeSearchDV.COL_NAME_ACTIVE), Byte()))
                    e.Item.Cells(Me.GRID_COL_ACTIVE).Text = dvRow(VSCClassCode.ClassCodeSearchDV.COL_NAME_DESCRIPTION).ToString

                ElseIf (itemType = ListItemType.EditItem) Then

                    e.Item.Cells(Me.GRID_COL_CLASS_CODE_ID).Text = GetGuidStringFromByteArray(CType(dvRow(VSCClassCode.ClassCodeSearchDV.COL_NAME_VSC_CLASS_CODE_ID), Byte()))
                    CType(e.Item.Cells(Me.GRID_COL_CLASS_CODE).FindControl(GRID_NAME_CLASS_CODE), TextBox).Text = dvRow(VSCClassCode.ClassCodeSearchDV.COL_NAME_CODE).ToString

                    Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

                    'Me.BindListControlToDataView(CType(e.Item.Cells(Me.GRID_COL_ACTIVE).FindControl(GRID_NAME_ACTIVE), DropDownList), LookupListNew.DropdownLookupList("YESNO", langId, True))
                    CType(e.Item.Cells(Me.GRID_COL_ACTIVE).FindControl(GRID_NAME_ACTIVE), DropDownList).Populate(CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                           {
                                            .AddBlankItem = True
                                           })
                    Me.SetSelectedItem(CType(e.Item.Cells(Me.GRID_COL_ACTIVE).FindControl(GRID_NAME_ACTIVE), DropDownList), Me.State.MyBO.Active)

                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try

        End Sub
        Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moDataGrid.PageIndexChanged

            Try
                If (Not (Me.State.IsEditMode)) Then
                    Me.State.PageIndex = e.NewPageIndex
                    Me.moDataGrid.CurrentPageIndex = Me.State.PageIndex
                    Me.PopulateGrid()
                    Me.moDataGrid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try

        End Sub

        Protected Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)

            Try
                Dim index As Integer = e.Item.ItemIndex

                If (e.CommandName = Me.EDIT_COMMAND) Then
                    'Do the Edit here

                    'Set the IsEditMode flag to TRUE
                    Me.State.IsEditMode = True

                    Me.State.ClassCodeId = New Guid(Me.moDataGrid.Items(e.Item.ItemIndex).Cells(Me.GRID_COL_CLASS_CODE_ID).Text)

                    Me.State.MyBO = New VSCClassCode(Me.State.ClassCodeId)

                    Me.PopulateGrid()

                    Me.State.PageIndex = moDataGrid.CurrentPageIndex

                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(Me.moDataGrid, False)

                    'Set focus on the Dealer dropdown list for the EditItemIndex row
                    Me.SetFocusOnEditableFieldInGrid(Me.moDataGrid, Me.GRID_COL_CLASS_CODE, index)

                    Me.SetButtonsState()

                ElseIf (e.CommandName = Me.DELETE_COMMAND) Then
                    'Do the delete here

                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    moDataGrid.SelectedIndex = Me.NO_ROW_SELECTED_INDEX

                    'Save the Id in the Session
                    Me.State.ClassCodeId = New Guid(Me.moDataGrid.Items(e.Item.ItemIndex).Cells(Me.GRID_COL_CLASS_CODE_ID).Text)
                    Me.State.MyBO = New VSCClassCode(Me.State.ClassCodeId)

                    Try
                        Me.State.MyBO.Delete()
                        'Call the Save() method in the Business Object here
                        Me.State.MyBO.Save()
                    Catch ex As Exception
                        Me.State.MyBO.RejectChanges()
                        Throw ex
                    End Try

                    Me.State.PageIndex = moDataGrid.CurrentPageIndex

                    'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
                    Me.State.IsAfterSave = True

                    Me.State.searchDV = Nothing
                    Me.PopulateDropdowns()
                    PopulateGrid()
                    Me.State.PageIndex = moDataGrid.CurrentPageIndex
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try

        End Sub

        Protected Sub ItemBound(ByVal source As Object, ByVal e As DataGridItemEventArgs) Handles moDataGrid.ItemDataBound
            BaseItemBound(source, e)
        End Sub

        Protected Sub ItemCreated(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                moDataGrid.CurrentPageIndex = NewCurrentPageIndex(moDataGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try
        End Sub

        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles moDataGrid.SortCommand

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
                Me.State.PageIndex = 0
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try
        End Sub


        Protected Sub BindBoPropertiesToGridHeaders()

            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "active", Me.moDataGrid.Columns(Me.GRID_COL_ACTIVE))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "ClassCode", Me.moDataGrid.Columns(Me.GRID_COL_CLASS_CODE))

            Me.ClearGridHeadersAndLabelsErrSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As DataGrid, ByVal cellPosition As Integer, ByVal itemIndex As Integer)

            ''Set focus on the specified control on the EditItemIndex row for the grid

            Dim ctrlActive As DropDownList = CType(grid.Items(itemIndex).Cells(Me.GRID_COL_ACTIVE).FindControl(Me.GRID_NAME_ACTIVE), DropDownList)
            Me.SetSelectedItem(ctrlActive, Me.State.MyBO.Active)

            Dim ctrlCode As TextBox = CType(grid.Items(itemIndex).Cells(Me.GRID_COL_CLASS_CODE).FindControl(Me.GRID_NAME_CLASS_CODE), TextBox)
            SetFocus(ctrlCode)




        End Sub


#End Region
    End Class
End Namespace