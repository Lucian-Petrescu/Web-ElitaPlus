Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace VSC

    Partial Class vscCoverageLimitForm
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
        Protected WithEvents moTitleLabel As System.Web.UI.WebControls.Label
        Protected WithEvents moTitleLabel2 As System.Web.UI.WebControls.Label
        Protected WithEvents moErrorController As ErrorController
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
        Public Const GRID_COL_COVERAGE_LIMIT_ID As Integer = 1
        Public Const GRID_COL_COVERAGE_CODE As Integer = 2
        Public Const GRID_COL_COVERAGE_TYPE_ID As Integer = 3
        Public Const GRID_COL_DESCRIPTION As Integer = 4
        Public Const GRID_COL_MONTHS As Integer = 5
        Public Const GRID_COL_KM As Integer = 6

        Private Const EDIT_COMMAND As String = "EditRecord"
        Private Const SORT_COMMAND As String = "Sort"

        Private Const GRID_NAME_MONTHS As String = "moMonthsTextGrid"
        Private Const GRID_NAME_KM As String = "moKmTextGrid"


        Private Const NO_ROW_SELECTED_INDEX As Integer = -1
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

#End Region

#Region "Page State"
        Class MyState

            Public PageIndex As Integer = 0
            '   Public MyBO As VSCCoverageLimit = New VSCCoverageLimit
            Public MyBO As VSCCoverageLimit
            Public SearchLimitMask As String
            Public SearchCoverageTypeId As Guid = Guid.Empty
            Public SearchKmMask As String
            Public SearchMonthsMask As String

            Public SortExpression As String = VSCCoverageLimit.CoverageLimitSearchDV.COL_NAME_COVERAGE_LIMIT_CODE

            Public CoverageLimitId As Guid = Guid.Empty
            Public LangId As Guid
            Public CompanyId As Guid

            Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE

            Public IsGridVisible As Boolean
            Public IsAfterSave As Boolean
            Public IsEditMode As Boolean

            Public searchDV As DataView = Nothing
            Public HasDataChanged As Boolean
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

#Region "Page_Events"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

            Try
                moErrorController.Clear_Hide()
                If Not Page.IsPostBack Then
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    SetDefaultButton(moLimitCodeTx, SearchButton)
                    SetDefaultButton(moMonthTx, SearchButton)
                    SetDefaultButton(moKmTx, SearchButton)
                    SetGridItemStyleColor(moDataGrid)
                    If State.MyBO Is Nothing Then
                        State.MyBO = New VSCCoverageLimit
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
            PopulateGrid()
            State.PageIndex = moDataGrid.CurrentPageIndex
            SetButtonsState()

        End Sub
        Private Sub SetButtonsState()
            If (State.IsEditMode) Then
                ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, True)
                ControlMgr.SetVisibleControl(Me, CancelButton, True)
                ControlMgr.SetEnableControl(Me, SearchButton, False)
                ControlMgr.SetEnableControl(Me, ClearButton, False)
                MenuEnabled = False
                If (cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, False)
                End If
            Else
                ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, False)
                ControlMgr.SetVisibleControl(Me, CancelButton, False)
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
        Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles moDataGrid.ItemDataBound
            Try
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

                If (itemType = ListItemType.Item OrElse _
                    itemType = ListItemType.AlternatingItem OrElse _
                    itemType = ListItemType.SelectedItem) Then

                    e.Item.Cells(GRID_COL_COVERAGE_LIMIT_ID).Text = GetGuidStringFromByteArray(CType(dvRow(VSCCoverageLimit.CoverageLimitSearchDV.COL_NAME_VSC_COVERAGE_LIMIT_ID), Byte()))
                    e.Item.Cells(GRID_COL_COVERAGE_CODE).Text = dvRow(VSCCoverageLimit.CoverageLimitSearchDV.COL_NAME_COVERAGE_LIMIT_CODE).ToString
                    e.Item.Cells(GRID_COL_COVERAGE_TYPE_ID).Text = GetGuidStringFromByteArray(CType(dvRow(VSCCoverageLimit.CoverageLimitSearchDV.COL_NAME_VSC_COVERAGE_TYPE_ID), Byte()))
                    e.Item.Cells(GRID_COL_DESCRIPTION).Text = dvRow(VSCCoverageLimit.CoverageLimitSearchDV.COL_NAME_COVERAGE_TYPE_DESC).ToString
                    e.Item.Cells(GRID_COL_MONTHS).Text = dvRow(VSCCoverageLimit.CoverageLimitSearchDV.COL_NAME_COVERAGE_MONTHS).ToString
                    e.Item.Cells(GRID_COL_KM).Text = dvRow(VSCCoverageLimit.CoverageLimitSearchDV.COL_NAME_COVERAGE_KM_MI).ToString

                ElseIf (itemType = ListItemType.EditItem) Then
                    e.Item.Cells(GRID_COL_COVERAGE_LIMIT_ID).Text = GetGuidStringFromByteArray(CType(dvRow(VSCCoverageLimit.CoverageLimitSearchDV.COL_NAME_VSC_COVERAGE_LIMIT_ID), Byte()))
                    e.Item.Cells(GRID_COL_COVERAGE_TYPE_ID).Text = GetGuidStringFromByteArray(CType(dvRow(VSCCoverageLimit.CoverageLimitSearchDV.COL_NAME_COVERAGE_TYPE_ID), Byte()))
                    e.Item.Cells(GRID_COL_COVERAGE_CODE).Text = dvRow(VSCCoverageLimit.CoverageLimitSearchDV.COL_NAME_COVERAGE_LIMIT_CODE).ToString
                    e.Item.Cells(GRID_COL_DESCRIPTION).Text = dvRow(VSCCoverageLimit.CoverageLimitSearchDV.COL_NAME_COVERAGE_TYPE_DESC).ToString

                    CType(e.Item.Cells(GRID_COL_MONTHS).FindControl(GRID_NAME_MONTHS), TextBox).Text = dvRow(VSCCoverageLimit.CoverageLimitSearchDV.COL_NAME_COVERAGE_MONTHS).ToString
                    CType(e.Item.Cells(GRID_COL_KM).FindControl(GRID_NAME_KM), TextBox).Text = dvRow(VSCCoverageLimit.CoverageLimitSearchDV.COL_NAME_COVERAGE_KM_MI).ToString


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
                    'Edit Mode
                    State.IsEditMode = True

                    State.CoverageLimitId = New Guid(moDataGrid.Items(e.Item.ItemIndex).Cells(GRID_COL_COVERAGE_LIMIT_ID).Text)

                    State.MyBO = New VSCCoverageLimit(State.CoverageLimitId)

                    PopulateGrid()

                    State.PageIndex = moDataGrid.CurrentPageIndex

                    'Disable all Edit icon buttons on the Grid
                    SetGridControls(moDataGrid, False)

                    'Set focus on the Dealer dropdown list for the EditItemIndex row
                    SetFocusOnEditableFieldInGrid(moDataGrid, GRID_COL_COVERAGE_LIMIT_ID, index)

                    SetButtonsState()

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

            BindBOPropertyToGridHeader(State.MyBO, "CoverageKmMi", moDataGrid.Columns(GRID_COL_KM))
            BindBOPropertyToGridHeader(State.MyBO, "CoverageMonths", moDataGrid.Columns(GRID_COL_MONTHS))
            BindBOPropertyToGridHeader(State.MyBO, "CoverageTypeId", moDataGrid.Columns(GRID_COL_COVERAGE_TYPE_ID))
            BindBOPropertyToGridHeader(State.MyBO, "CoverageLimitCode", moDataGrid.Columns(GRID_COL_COVERAGE_CODE))

            ClearGridHeadersAndLabelsErrSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(grid As DataGrid, cellPosition As Integer, itemIndex As Integer)

            ''Set focus on the specified control on the EditItemIndex row for the grid
            Dim ctrlMonths As TextBox = CType(grid.Items(itemIndex).Cells(GRID_COL_MONTHS).FindControl(GRID_NAME_MONTHS), TextBox)
            SetFocus(ctrlMonths)


        End Sub



#End Region

#Region "Populate Handlers"


        Private Sub PopulateDropdowns()

            Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

            Try

                'Me.BindListControlToDataView(moCoverageTypeDrop, LookupListNew.GetCoverageTypeByCompanyGroupLookupList(oLanguageId, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, False), , , True)

                Dim CoverageTypes As DataElements.ListItem() =
                    CommonConfigManager.Current.ListManager.GetList(listCode:="CoverageTypeByCompanyGroup",
                                                                    languageCode:=Thread.CurrentPrincipal.GetLanguageCode(),
                                                                    context:=New ListContext() With
                                                                    {
                                                                      .CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                                                                    })

                moCoverageTypeDrop.Populate(CoverageTypes.ToArray(),
                                        New PopulateOptions() With
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
                    State.searchDV = VSCCoverageLimit.getList(moLimitCodeTx.Text, GetSelectedItem(moCoverageTypeDrop), moMonthTx.Text, moKmTx.Text)
                End If


                moDataGrid.AutoGenerateColumns = False
                If (State.IsAfterSave) Then
                    State.IsAfterSave = False
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.CoverageLimitId, moDataGrid, State.PageIndex)
                ElseIf (State.IsEditMode) Then
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.CoverageLimitId, moDataGrid, State.PageIndex, State.IsEditMode)
                Else
                    SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, moDataGrid, State.PageIndex)
                End If

                State.searchDV.Sort = State.SortExpression
                moDataGrid.Columns(GRID_COL_COVERAGE_CODE).SortExpression = VSCCoverageLimit.CoverageLimitSearchDV.COL_NAME_COVERAGE_LIMIT_CODE
                moDataGrid.Columns(GRID_COL_DESCRIPTION).SortExpression = VSCCoverageLimit.CoverageLimitSearchDV.COL_NAME_COVERAGE_TYPE_DESC

                SortAndBindGrid()

            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try

        End Sub

        Private Sub SortAndBindGrid()

            TranslateGridControls(moDataGrid)
            State.PageIndex = moDataGrid.CurrentPageIndex
            moDataGrid.DataSource = State.searchDV
            HighLightSortColumn(moDataGrid, State.SortExpression)
            moDataGrid.DataBind()

            ControlMgr.SetVisibleControl(Me, moDataGrid, State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, moDataGrid.Visible)

            Session("recCount") = State.searchDV.Count

            If State.searchDV.Count > 0 Then

                If moDataGrid.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            Else
                If moDataGrid.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If

            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, moDataGrid)

        End Sub


        Private Sub PopulateBOFromForm()

            With State.MyBO

                PopulateBOProperty(State.MyBO, "CoverageKmMi", CType(GetSelectedGridControl(moDataGrid, GRID_COL_KM), TextBox))
                PopulateBOProperty(State.MyBO, "CoverageMonths", CType(GetSelectedGridControl(moDataGrid, GRID_COL_MONTHS), TextBox))

            End With
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If

        End Sub

#End Region

#Region "Button Click Handlers"

        Private Sub ClearButton_Click(sender As System.Object, e As System.EventArgs) Handles ClearButton.Click

            Try
                moLimitCodeTx.Text = String.Empty
                moMonthTx.Text = String.Empty
                moKmTx.Text = String.Empty
                moCoverageTypeDrop.SelectedIndex = 0

                moDataGrid.CurrentPageIndex = NO_PAGE_INDEX

                moDataGrid.DataSource = Nothing
                moDataGrid.DataBind()

                ControlMgr.SetVisibleControl(Me, trPageSize, False)

                With State
                    .SearchLimitMask = moLimitCodeTx.Text
                    .SearchCoverageTypeId = Guid.Empty
                    .SearchMonthsMask = moMonthTx.Text
                    .SearchKmMask = moKmTx.Text

                End With
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub SearchButton_Click(sender As System.Object, e As System.EventArgs) Handles SearchButton.Click

            Try
                State.PageIndex = 0
                State.CoverageLimitId = Guid.Empty
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

                ReturnFromEditing()
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



    End Class

End Namespace
