Option Strict On
Option Explicit On

Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace VSC

    Partial Class VscCoverageLimitListForm
        Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Protected WithEvents moErrorController As ErrorController
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
            '  Public MyBO As VSCCoverageLimit = New VSCCoverageLimit
            Public MyBO As VSCCoverageLimit
            Public SearchLimitMask As String
            Public SearchCoverageTypeId As Guid = Guid.Empty
            Public SearchKmMask As String
            Public SearchMonthsMask As String

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

            ' Public SortExpression As String = Coveragelimit.
            'Public AddingNewRow As Boolean


            'Private mnPageIndex As Integer = 0
            'Private msPageSort As String
            'Private mnPageSize As Integer = DEFAULT_PAGE_SIZE
            'Public searchDV As PoliceStation.PoliceStationSearchDV = Nothing
            'Public SortExpression As String = PoliceStation.PoliceStationSearchDV.COL_POLICE_STATION_NAME

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


#Region "Properties"

        Public ReadOnly Property IsEditing() As Boolean
            Get
                IsEditing = (Me.moDataGrid.EditItemIndex > NO_ROW_SELECTED_INDEX)
            End Get
        End Property

#End Region

#Region "Page_Events"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Try
                moErrorController.Clear_Hide()
                If Not Page.IsPostBack Then
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Me.SetDefaultButton(Me.moLimitCodeTx, Me.SearchButton)
                    Me.SetDefaultButton(Me.moMonthTx, Me.SearchButton)
                    Me.SetDefaultButton(Me.moKmTx, Me.SearchButton)
                    Me.SetGridItemStyleColor(Me.moDataGrid)
                    If Me.State.MyBO Is Nothing Then
                        Me.State.MyBO = New VSCCoverageLimit
                    End If
                    Me.PopulateFormFromBOs()
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
            Me.PopulateGrid()
            Me.State.PageIndex = moDataGrid.CurrentPageIndex
            SetButtonsState()

        End Sub
        Private Sub SetButtonsState()
            If (Me.State.IsEditMode) Then
                ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, True)
                ControlMgr.SetVisibleControl(Me, CancelButton, True)
                ControlMgr.SetEnableControl(Me, SearchButton, False)
                ControlMgr.SetEnableControl(Me, ClearButton, False)
                Me.EnableDisableControls(moPanel, True)
                Me.MenuEnabled = False
                If (Me.cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, False)
                End If
            Else
                ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, False)
                ControlMgr.SetVisibleControl(Me, CancelButton, False)
                ControlMgr.SetEnableControl(Me, SearchButton, True)
                ControlMgr.SetEnableControl(Me, ClearButton, True)
                Me.EnableDisableControls(moPanel, False)
                Me.MenuEnabled = True
                If (Me.cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, True)
                End If
            End If

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

                    e.Item.Cells(Me.GRID_COL_COVERAGE_LIMIT_ID).Text = GetGuidStringFromByteArray(CType(dvRow(VSCCoverageLimit.CoverageLimitSearchDV.COL_NAME_VSC_COVERAGE_LIMIT_ID), Byte()))
                    e.Item.Cells(Me.GRID_COL_COVERAGE_CODE).Text = dvRow(VSCCoverageLimit.CoverageLimitSearchDV.COL_NAME_COVERAGE_LIMIT_CODE).ToString
                    e.Item.Cells(Me.GRID_COL_COVERAGE_TYPE_ID).Text = GetGuidStringFromByteArray(CType(dvRow(VSCCoverageLimit.CoverageLimitSearchDV.COL_NAME_VSC_COVERAGE_TYPE_ID), Byte()))
                    e.Item.Cells(Me.GRID_COL_DESCRIPTION).Text = dvRow(VSCCoverageLimit.CoverageLimitSearchDV.COL_NAME_COVERAGE_TYPE_DESC).ToString
                    e.Item.Cells(Me.GRID_COL_MONTHS).Text = dvRow(VSCCoverageLimit.CoverageLimitSearchDV.COL_NAME_COVERAGE_MONTHS).ToString
                    e.Item.Cells(Me.GRID_COL_KM).Text = dvRow(VSCCoverageLimit.CoverageLimitSearchDV.COL_NAME_COVERAGE_KM_MI).ToString

                ElseIf (itemType = ListItemType.EditItem) Then
                    e.Item.Cells(Me.GRID_COL_COVERAGE_LIMIT_ID).Text = GetGuidStringFromByteArray(CType(dvRow(VSCCoverageLimit.CoverageLimitSearchDV.COL_NAME_VSC_COVERAGE_LIMIT_ID), Byte()))
                    e.Item.Cells(Me.GRID_COL_COVERAGE_TYPE_ID).Text = GetGuidStringFromByteArray(CType(dvRow(VSCCoverageLimit.CoverageLimitSearchDV.COL_NAME_COVERAGE_TYPE_ID), Byte()))
                    'CType(e.Item.Cells(Me.GRID_COL_DESCRIPTION).FindControl(GRID_NAME_COVERAGETYPE), TextBox).Text = dvRow(VSCCoverageLimit.CoverageLimitSearchDV.COL_NAME_COVERAGE_TYPE_DESC).ToString
                    e.Item.Cells(Me.GRID_COL_COVERAGE_CODE).Text = dvRow(VSCCoverageLimit.CoverageLimitSearchDV.COL_NAME_COVERAGE_LIMIT_CODE).ToString
                    e.Item.Cells(Me.GRID_COL_DESCRIPTION).Text = dvRow(VSCCoverageLimit.CoverageLimitSearchDV.COL_NAME_COVERAGE_TYPE_DESC).ToString

                    CType(e.Item.Cells(Me.GRID_COL_MONTHS).FindControl(GRID_NAME_MONTHS), TextBox).Text = dvRow(VSCCoverageLimit.CoverageLimitSearchDV.COL_NAME_COVERAGE_MONTHS).ToString
                    CType(e.Item.Cells(Me.GRID_COL_KM).FindControl(GRID_NAME_KM), TextBox).Text = dvRow(VSCCoverageLimit.CoverageLimitSearchDV.COL_NAME_COVERAGE_KM_MI).ToString


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
                    'Edit Mode
                    Me.State.IsEditMode = True

                    Me.State.CoverageLimitId = New Guid(Me.moDataGrid.Items(e.Item.ItemIndex).Cells(Me.GRID_COL_COVERAGE_LIMIT_ID).Text)

                    Me.State.MyBO = New VSCCoverageLimit(Me.State.CoverageLimitId)

                    Me.PopulateGrid()

                    Me.State.PageIndex = moDataGrid.CurrentPageIndex

                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(Me.moDataGrid, False)

                    'Set focus on the Dealer dropdown list for the EditItemIndex row
                    Me.SetFocusOnEditableFieldInGrid(Me.moDataGrid, Me.GRID_COL_COVERAGE_LIMIT_ID, index)

                    Me.SetButtonsState()

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

            'Try
            '    If Me.State.SortExpression.StartsWith(e.SortExpression) Then
            '        If Me.State.SortExpression.EndsWith(" DESC") Then
            '            Me.State.SortExpression = e.SortExpression
            '        Else
            '            Me.State.SortExpression &= " DESC"
            '        End If
            '    Else
            '        Me.State.SortExpression = e.SortExpression
            '    End If
            '    'To handle the requirement of always going to the FIRST page on the Grid whenever the user switches the sorting criterion
            '    'Set the Me.State.selectedClaimId = Guid.Empty and set Me.State.PageIndex = 0
            '    Me.State.Id = Guid.Empty
            '    Me.State.PageIndex = 0

            '    Me.PopulateGrid()
            'Catch ex As Exception
            '    Me.HandleErrors(ex, Me.moErrorController)
            'End Try

        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()

            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "CoverageKmMi", Me.moDataGrid.Columns(Me.GRID_COL_KM))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "CoverageMonths", Me.moDataGrid.Columns(Me.GRID_COL_MONTHS))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "CoverageTypeId", Me.moDataGrid.Columns(Me.GRID_COL_COVERAGE_TYPE_ID))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "CoverageLimitCode", Me.moDataGrid.Columns(Me.GRID_COL_COVERAGE_CODE))

            Me.ClearGridHeadersAndLabelsErrSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As DataGrid, ByVal cellPosition As Integer, ByVal itemIndex As Integer)

            ''Set focus on the specified control on the EditItemIndex row for the grid
            '  Dim ctrlKm As TextBox = CType(grid.Items(itemIndex).Cells(Me.GRID_COL_MONTHS).FindControl(GRID_NAME_MONTHS), TextBox)
            'CType(grid.Items(itemIndex).Cells(cellPosition + 2).FindControl(DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox)()
            '     SetFocus(ctrlKm)

            'Dim ctrlMonths As TextBox = CType(grid.Items(itemIndex).Cells(Me.GRID_COL_KM).FindControl((GRID_NAME_KM), TextBox)
            'ctrlDescription.Style("width") = Me.DESCRIPTION_CELL_STYLE_WIDTH


        End Sub



#End Region

#Region "Populate Handlers"


        Private Sub PopulateFormFromBOs()

            Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

            Try
                'Dim oCoverageTypeView As DataView = LookupListNew.GetItemRiskTypeLookupList(oProductId)

                ' Me.BindListControlToDataView(moCoverageTypeDrop, LookupListNew.GetCoverageTypeByCompanyGroupLookupList(oLanguageId, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, False), , , True)

                Dim CoverageTypes As DataElements.ListItem() =
                    CommonConfigManager.Current.ListManager.GetList(listCode:="CoverageTypeByCompanyGroup",
                                                                    languageCode:=Thread.CurrentPrincipal.GetLanguageCode(),
                                                                    context:=New ListContext() With
                                                                    {
                                                                      .CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                                                                    })

                Me.moCoverageTypeDrop.Populate(CoverageTypes.ToArray(),
                                        New PopulateOptions() With
                                        {
                                            .AddBlankItem = True
                                        })

                'If IsPostBack And Not Me.State.IsUndo Then
                ' JLR - Restore Presviously Selected Values
                'BindSelectItem(Me.State.SearchCoverageTypeId.ToString, moCoverageTypeDrop)
                '   Me.State.IsUndo = False
                ' Else
                '     BindSelectItem(TheCoverage.CoverageTypeId.ToString, moCoverageTypeDrop)
                ' End If
                'PopulateRestCoverage()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try

        End Sub

        Public Sub PopulateGrid()

            Try
                If ((Me.State.searchDV Is Nothing) OrElse (Me.State.HasDataChanged)) Then
                    Me.State.searchDV = VSCCoverageLimit.getList(Me.moLimitCodeTx.Text, Me.GetSelectedItem(moCoverageTypeDrop), Me.moMonthTx.Text, Me.moKmTx.Text)
                End If
                'Me.State.searchDV.Sort = Me.State.SortExpression
                Me.moDataGrid.AutoGenerateColumns = False
                'Me.Grid.Columns(Me.GRID_COL_POLICE_STATION_CODE_IDX).SortExpression = PoliceStation.PoliceStationSearchDV.COL_POLICE_STATION_CODE
                'Me.Grid.Columns(Me.GRID_COL_POLICE_STATION_NAME_IDX).SortExpression = PoliceStation.PoliceStationSearchDV.COL_POLICE_STATION_NAME

                If (Me.State.IsEditMode) Then
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.CoverageLimitId, Me.moDataGrid, Me.State.PageIndex, Me.State.IsEditMode)
                Else
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.CoverageLimitId, Me.moDataGrid, Me.State.PageIndex)
                End If

                Me.SortAndBindGrid()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try

        End Sub

        Private Sub SortAndBindGrid()

            Me.TranslateGridControls(moDataGrid)
            Me.State.PageIndex = Me.moDataGrid.CurrentPageIndex
            Me.moDataGrid.DataSource = Me.State.searchDV
            '    HighLightSortColumn(moDataGrid, Me.State.SortExpression)
            Me.moDataGrid.DataBind()

            ControlMgr.SetVisibleControl(Me, moDataGrid, Me.State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Me.moDataGrid.Visible)

            Session("recCount") = Me.State.searchDV.Count

            If Me.State.searchDV.Count > 0 Then

                If Me.moDataGrid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            Else
                If Me.moDataGrid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If

            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, moDataGrid)

        End Sub


        Private Sub PopulateBOFromForm()

            With Me.State.MyBO

                Me.PopulateBOProperty(Me.State.MyBO, "CoverageKmMi", CType(Me.GetSelectedGridControl(moDataGrid, GRID_COL_KM), TextBox))
                Me.PopulateBOProperty(Me.State.MyBO, "CoverageMonths", CType(Me.GetSelectedGridControl(moDataGrid, GRID_COL_MONTHS), TextBox))

            End With
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If

        End Sub

#End Region


#Region "Button Click Handlers"

        Private Sub ClearButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearButton.Click

            Try
                moLimitCodeTx.Text = String.Empty
                moMonthTx.Text = String.Empty
                moKmTx.Text = String.Empty
                Me.moCoverageTypeDrop.SelectedIndex = 0

                With Me.State
                    .SearchLimitMask = moLimitCodeTx.Text
                    .SearchCoverageTypeId = Guid.Empty
                    .SearchMonthsMask = moMonthTx.Text
                    .SearchKmMask = moKmTx.Text

                End With
            Catch ex As Exception
                Me.HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub SearchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchButton.Click

            Try
                Me.State.PageIndex = 0
                Me.State.CoverageLimitId = Guid.Empty
                Me.State.IsGridVisible = True
                Me.State.searchDV = Nothing
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)

            End Try

        End Sub
        Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click
            Try
                Me.moDataGrid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                Me.State.Canceling = True

                ReturnFromEditing()
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












    End Class

End Namespace
