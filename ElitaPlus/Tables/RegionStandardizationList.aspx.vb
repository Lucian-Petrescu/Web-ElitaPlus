Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Tables

    Partial Class RegionStandardizationList
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

#Region "Page State"
        Class MyState
            Public PageIndex As Integer = 0
            'Public RegionAliasBO As RegionStandardization = New RegionStandardization
            Public RegionAliasBO As RegionStandardization
            Public RegionAliasId As Guid
            Public LangId As Guid
            Public DescriptionMask As String
            Public RegionIdSearch As Guid
            Public CompanyId As Guid
            Public SearchCountryId As Guid = Guid.Empty
            Public IsAfterSave As Boolean
            Public IsEditMode As Boolean
            Public IsGridVisible As Boolean
            Public searchDV As DataView = Nothing
            Public SortExpression As String = RegionStandardization.COL_NAME_DESCRIPTION
            Public AddingNewRow As Boolean
            Public Canceling As Boolean
            Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
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
                IsEditing = (Me.moRegionGrid.EditItemIndex > NO_ROW_SELECTED_INDEX)
            End Get
        End Property

#End Region


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents moLblTitle As System.Web.UI.WebControls.Label
        Protected WithEvents moRadioBtnList As System.Web.UI.WebControls.RadioButtonList
        Protected WithEvents moErrorController As ErrorController
        Protected WithEvents moBtnEdit As System.Web.UI.WebControls.ImageButton
        Protected WithEvents moBtnDelete As System.Web.UI.WebControls.ImageButton

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

        Private Const GRID_COL_REGION_ALIAS_ID As Integer = 2
        Private Const GRID_COL_COUNTRY_ID As Integer = 3
        Private Const GRID_COL_COUNTRY As Integer = 4
        Private Const GRID_COL_REGION_ALIAS As Integer = 5
        Private Const GRID_COL_REGION As Integer = 6

        Private Const DBREGIONALIAS_ID As Integer = 0
        Private Const DBCOUNTRY_ID As Integer = 1
        Private Const DBREGIONALIAS As Integer = 2
        Private Const DBREGION As Integer = 3

        Private Const EDITIMG_COL As Integer = 0
        Private Const DELETEIMG_COL As Integer = 1
        Private Const REGIONALIAS_ID_COL As Integer = 2
        Private Const COUNTRY_ID_COL As Integer = 3
        Private Const REGIONALIAS_COL As Integer = 4
        Private Const REGION_COL As Integer = 5

        Private Const COUNTRY_IN_GRID_CONTROL_NAME As String = "cboCountryInGrid"
        Private Const DESCRIPTION_IN_GRID_CONTROL_NAME As String = "moRegionAliasText"
        Private Const REGION_LIST_IN_GRID_CONTROL_NAME As String = "moRegionDropdown"
        Private Const REGION_ALIAS_ID_IN_GRID_CONTROL_NAME As String = "moRegionAliasId"

        Private Const REGIONALIASID_CONTROL_NUM As Integer = 1
        Private Const COUNTRYID_CONTROL_NUM As Integer = 1
        Private Const REGIONALIAS_LABEL_CONTROL_NUM As Integer = 1
        Private Const REGIONALIAS_TEXT_CONTROL_NUM As Integer = 3
        Private Const REGION_LABEL_CONTROL_NUM As Integer = 1
        Private Const REGION_DROP_DOWN_CONTROL_NUM As Integer = 3
        Private Const REGIONALIAS_CONTROL_NAME As String = "moRegionAliasId"

        Private Const PAGE_SIZE As Integer = 10

        Private Const DATAGRID_EDIT_BUTTON_NAME As String = "EditButton_WRITE"
        Private Const DATAGRID_DELETE_BUTTON_NAME As String = "DeleteButton_WRITE"
        Private Const EDIT_COMMAND As String = "EditButton"
        Private Const DELETE_COMMAND As String = "DeleteButton"
        Private Const SORT_COMMAND As String = "Sort"
        Private Const MGS_CONFIRM_PROMPT As String = "MGS_CONFIRM_PROMPT" '"Are you sure you want to delete the selected records?"
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED" '"The record has not been saved because the current record has not been changed"

        Private Const REGION_STANDARD_FORM001 As String = "REGION_STANDARD_FORM001" ' Region Standardization Grid Exception
        Private Const REGION_STANDARD_FORM002 As String = "REGION_STANDARD_FORM002" ' Region Standardization Update/Delete Exception
        Private Const REGION_STANDARD_FORM003 As String = "REGION_STANDARD_FORM003" ' Region Standardization Exception due to Cancel of Edit 
        Private Const REGION_STANDARD_FORM004 As String = "REGION_STANDARD_FORM004" ' Region Standardization New Region Code Exception 
        Private Const REGION_STANDARD_FORM005 As String = "REGION_STANDARD_FORM005" '"The system encountered some difficulties while trying to update the data"
        Private Const MSG_UNIQUE_VIOLATION As String = "MSG_DUPLICATE_REGION_ALIAS" '"Duplicate Region Alias"
        Private Const UNIQUE_VIOLATION As String = "unique constraint"
        Private Const MSG_REQUIRED_VIOLATION As String = "MSG_REQUIRED_REGION_ALIAS" '"Required Region Alias"
        Private Const REQUIRED_VIOLATION As String = "ORA-01400"
        Private Const mandatoryField As String = "ORA-01400"
        Private Const uniqueFueld As String = "ORA-00001"

        Private Const NO_ROW_SELECTED_INDEX As Integer = -1
        Private Const FIRST_ROW_SELECTED_INDEX As Integer = 1

#End Region

#Region "Handlers"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                'moErrorController.Clear_Hide()
                'If Not Page.IsPostBack Then
                '    Me.trPageSize.Visible = False
                '    Me.SetGridItemStyleColor(moRegionGrid)
                '    Me.ShowMissingTranslations(ErrController)
                '    PopulateRegionDropdown(moDropdownRegion, True)
                '    SetButtonsState()
                moErrorController.Clear_Hide()
                Me.SetStateProperties()
                If Not Page.IsPostBack Then
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Me.SetDefaultButton(Me.moTxtRegionStandardSearch, Me.moBtnSearch)
                    Me.SetGridItemStyleColor(Me.moRegionGrid)
                    If Me.State.RegionAliasBO Is Nothing Then
                        Me.State.RegionAliasBO = New RegionStandardization
                    End If
                    'DEF-1692
                    Dim compCount As Integer
                    compCount = ElitaPlusIdentity.Current.ActiveUser.Countries.Count
                    If compCount = 1 Then
                        PopulateRegionDropdown(moDropdownRegion, True, ElitaPlusIdentity.Current.ActiveUser.Companies)
                    Else
                        moCountryDrop.AutoPostBack = True
                    End If
                    'End of DEF-1692
                    Me.State.PageIndex = 0
                    SetButtonsState()
                    PopulateCountry(moCountryDrop)
                End If
                BindBoPropertiesToGridHeaders()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try
            Me.ShowMissingTranslations(moErrorController)
        End Sub

        Private Sub moBtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnSearch.Click
            Try
                Me.State.IsGridVisible = True
                Me.State.searchDV = Nothing
                PopulateRegionGrid()
                Me.State.PageIndex = moRegionGrid.CurrentPageIndex
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try
        End Sub

        Private Sub moBtnClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnClearSearch.Click
            Try
                moTxtRegionStandardSearch.Text = String.Empty
                moDropdownRegion.SelectedIndex = Me.BLANK_ITEM_SELECTED
                moCountryDrop.SelectedIndex = Me.BLANK_ITEM_SELECTED

                'Update Page State
                With Me.State
                    .DescriptionMask = moTxtRegionStandardSearch.Text
                    .RegionIdSearch = Nothing
                    .SearchCountryId = Nothing
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try
        End Sub

        Private Sub moBtnAdd_WITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnAdd_WRITE.Click
            'AddRegionAlias()
            'EditModeButtonsControl(IsEdit, False)
            Try
                Me.State.IsEditMode = True
                Me.State.IsGridVisible = True
                Me.State.AddingNewRow = True
                Me.AddRegionAlias()
                Me.SetButtonsState()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try
        End Sub

        Private Sub moBtnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnSave_WRITE.Click
            'Try
            '    If ApplyDataChanges() = True Then
            '        Me.AddInfoMsg(MSG_RECORD_SAVED_OK)
            '        'Me.State.searchDV = Nothing
            '        ReturnFromEditing()
            '        moRegionGrid.SelectedIndex = -1
            '    End If
            'Catch ex As Exception
            '    moErrorController.AddError(ApplicationMessages.GetApplicationMessage(REGION_STANDARD_FORM001, MyBase.GetApplicationUser.LanguageID))
            '    moErrorController.AddError(ex.Message)
            '    moErrorController.Show()
            'End Try
            Try
                PopulateBOFromForm()
                If (Me.State.RegionAliasBO.IsDirty) Then
                    Me.State.RegionAliasBO.Save()
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

        Private Sub moBtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnCancel.Click
            Try
                Me.moRegionGrid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
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

#End Region

#Region "Button Management"

        Private Sub PopulateRegionGrid()
            Dim dv As DataView
            Try
                If (Me.State.searchDV Is Nothing) Then
                    Me.State.searchDV = GetRegionGridDataView()
                End If
                'Ticket # 748479 - Search grids in Tables tab should not show pop-up message when number of retrieved record is over 1,000
                'Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
                If (Me.State.IsAfterSave) Then
                    Me.State.IsAfterSave = False
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.RegionAliasId, Me.moRegionGrid, Me.State.PageIndex)
                ElseIf (Me.State.IsEditMode) Then
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.RegionAliasId, Me.moRegionGrid, Me.State.PageIndex, Me.State.IsEditMode)
                Else
                    'For a rare Delete scenario...
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.moRegionGrid, Me.State.PageIndex)
                End If

                Me.moRegionGrid.AutoGenerateColumns = False
                Me.moRegionGrid.Columns(Me.GRID_COL_COUNTRY).SortExpression = RegionStandardization.RegionStandardizationSearchDV.COL_COUNTRY_NAME
                Me.moRegionGrid.Columns(Me.GRID_COL_REGION_ALIAS).SortExpression = RegionStandardization.COL_NAME_DESCRIPTION
                Me.moRegionGrid.Columns(Me.GRID_COL_REGION).SortExpression = RegionStandardization.COL_NAME_REGION

                SortAndBindGrid()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try
        End Sub

        Private Sub SortAndBindGrid()
            Me.State.PageIndex = Me.moRegionGrid.CurrentPageIndex
            Me.TranslateGridControls(moRegionGrid)
            moRegionGrid.DataSource = Me.State.searchDV
            HighLightSortColumn(moRegionGrid, Me.State.SortExpression)
            Me.moRegionGrid.DataBind()

            ControlMgr.SetVisibleControl(Me, moRegionGrid, Me.State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Me.moRegionGrid.Visible)

            Session("recCount") = Me.State.searchDV.Count

            If Me.moRegionGrid.Visible Then
                If (Me.State.AddingNewRow) Then
                    Me.lblRecordCount.Text = (Me.State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, moRegionGrid)
        End Sub

        Private Function GetRegionGridDataView() As DataView
            With State
                If .SearchCountryId.Equals(Guid.Empty) Then
                    Me.State.searchDV = RegionStandardization.GetRegionAliasList(Me.State.DescriptionMask,
                                                                                 Me.State.RegionIdSearch,
                                                                                 ElitaPlusIdentity.Current.ActiveUser.Companies)
                Else
                    Me.State.searchDV = RegionStandardization.GetRegionAliasList(Me.State.DescriptionMask,
                                                                                Me.State.RegionIdSearch,
                                                                                Me.State.SearchCountryId)
                End If
            End With

            Me.State.searchDV.Sort = moRegionGrid.DataMember()
            moRegionGrid.DataSource = Me.State.searchDV

            Return (Me.State.searchDV)
        End Function

        Private Sub SetStateProperties()
            Dim company As New ElitaPlus.BusinessObjectsNew.Company(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
            Dim countryId As Guid = company.BusinessCountryId

            Me.State.DescriptionMask = moTxtRegionStandardSearch.Text
            If (Not moDropdownRegion.SelectedItem Is Nothing AndAlso moDropdownRegion.SelectedItem.Value <> Me.NOTHING_SELECTED_TEXT) Then
                Me.State.RegionIdSearch = Me.GetGuidFromString(moDropdownRegion.SelectedItem.Value)
            Else
                Me.State.RegionIdSearch = Nothing
            End If
            Me.State.CompanyId = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID
            Me.State.SearchCountryId = Me.GetSelectedItem(moCountryDrop)

        End Sub

        Private Sub PopulateFormFromBO()

            Dim gridRowIdx As Integer = Me.moRegionGrid.EditItemIndex
            Try
                With Me.State.RegionAliasBO
                    Dim ctrCountryDropDown As DropDownList = CType(moRegionGrid.Items(gridRowIdx).Cells(Me.GRID_COL_COUNTRY_ID).FindControl(Me.COUNTRY_IN_GRID_CONTROL_NAME), DropDownList)
                    If (Not .CountryId.Equals(Guid.Empty)) Then
                        Me.SetSelectedItem(ctrCountryDropDown, .CountryId)
                    End If

                    If moCountryDrop.Items.Count < 3 Then ' there is only one country
                        ctrCountryDropDown.SelectedIndex = 1
                    End If
                    ControlMgr.SetEnableControl(Me, ctrCountryDropDown, moCountryDrop.Items.Count > 2)

                    If (Not .Description Is Nothing) Then
                        CType(Me.moRegionGrid.Items(gridRowIdx).Cells(Me.GRID_COL_REGION_ALIAS).FindControl(Me.DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox).Text = .Description
                    End If
                    If (Not .RegionId.Equals(Guid.Empty)) Then
                        Me.SetSelectedItem(CType(moRegionGrid.Items(gridRowIdx).Cells(Me.GRID_COL_REGION).FindControl(Me.REGION_LIST_IN_GRID_CONTROL_NAME), DropDownList), .RegionId)
                    End If
                    CType(Me.moRegionGrid.Items(gridRowIdx).Cells(Me.GRID_COL_REGION_ALIAS_ID).FindControl(Me.REGION_ALIAS_ID_IN_GRID_CONTROL_NAME), Label).Text = .Id.ToString

                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try

        End Sub

        Private Sub PopulateBOFromForm()
            Try
                With Me.State.RegionAliasBO
                    .CountryId = Me.GetSelectedItem(CType(moRegionGrid.Items(moRegionGrid.EditItemIndex).Cells(Me.GRID_COL_COUNTRY).FindControl(COUNTRY_IN_GRID_CONTROL_NAME), DropDownList))
                    .Description = CType(Me.moRegionGrid.Items(Me.moRegionGrid.EditItemIndex).Cells(Me.GRID_COL_REGION_ALIAS).FindControl(Me.DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox).Text
                    .RegionId = Me.GetSelectedItem(CType(moRegionGrid.Items(moRegionGrid.EditItemIndex).Cells(Me.GRID_COL_REGION).FindControl(Me.REGION_LIST_IN_GRID_CONTROL_NAME), DropDownList))
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try

        End Sub

        'DEF-1692
        Private Sub PopulateRegionDropdown(ByVal oDropDownList As DropDownList, Optional ByVal edmode As Boolean = False, Optional ByVal myCountry As ArrayList = Nothing)
            Try
                'Dim oRegionList As DataView = LookupListNew.GetRegionLookupList(myCountry)
                'End of DEF-1692
                'Me.BindListControlToDataView(oDropDownList, oRegionList)

                Dim RegionList As New Collections.Generic.List(Of DataElements.ListItem)

                For Each Country_id As Guid In ElitaPlusIdentity.Current.ActiveUser.Countries
                    Dim Regions As DataElements.ListItem() =
                            CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.RegionsByCountry,
                                                                            context:=New ListContext() With
                                                                            {
                                                                              .CountryId = Country_id
                                                                            })

                    If Regions.Count > 0 Then
                        If Not RegionList Is Nothing Then
                            RegionList.AddRange(Regions)
                        Else
                            RegionList = Regions.Clone()
                        End If
                    End If
                Next

                oDropDownList.Populate(RegionList.ToArray(),
                                        New PopulateOptions() With
                                        {
                                            .AddBlankItem = True,
                                            .SortFunc = AddressOf PopulateOptions.GetExtendedCode
                                        })

            Catch ex As Exception
            End Try
        End Sub
        'DEF-1692
        Private Sub PopulateRegionDropdown(ByVal oDropDownList As DropDownList, ByVal myCountry As Guid, Optional ByVal edmode As Boolean = False)
            Try
                'Dim oRegionList As DataView = LookupListNew.GetRegionLookupList(myCountry)
                'Me.BindListControlToDataView(oDropDownList, oRegionList)

                Dim Regions As DataElements.ListItem() =
                            CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.RegionsByCountry,
                                                                            context:=New ListContext() With
                                                                            {
                                                                              .CountryId = myCountry
                                                                            })

                oDropDownList.Populate(Regions.ToArray(),
                                        New PopulateOptions() With
                                        {
                                            .AddBlankItem = True
                                        })
            Catch ex As Exception
            End Try
        End Sub 'End of DEF-1692



        Private Sub AddRegionAlias()

            Me.State.searchDV = GetRegionGridDataView()

            Me.State.RegionAliasBO = New RegionStandardization
            Me.State.RegionAliasId = Me.State.RegionAliasBO.Id

            GetNewDataViewRow(Me.State.searchDV, Me.State.RegionAliasId)

            moRegionGrid.DataSource = Me.State.searchDV
            SetGridControls(Me.moRegionGrid, False)
            Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.RegionAliasId, Me.moRegionGrid, Me.State.PageIndex, Me.State.IsEditMode)

            Me.moRegionGrid.AutoGenerateColumns = False
            Me.moRegionGrid.Columns(Me.GRID_COL_COUNTRY).SortExpression = RegionStandardization.RegionStandardizationSearchDV.COL_COUNTRY_NAME
            Me.moRegionGrid.Columns(Me.GRID_COL_REGION_ALIAS).SortExpression = RegionStandardization.COL_NAME_DESCRIPTION
            Me.moRegionGrid.Columns(Me.GRID_COL_REGION).SortExpression = RegionStandardization.COL_NAME_REGION

            Me.SortAndBindGrid()

            'Set focus on the Description TextBox for the EditItemIndex row
            If moCountryDrop.Items.Count < 3 Then   'Only one country.
                Me.SetFocusOnEditableFieldInGrid(Me.moRegionGrid, Me.GRID_COL_REGION_ALIAS, moRegionGrid.EditItemIndex, Me.State.AddingNewRow And True)
            Else
                Me.SetFocusOnEditableFieldInGrid(Me.moRegionGrid, Me.GRID_COL_COUNTRY_ID, moRegionGrid.EditItemIndex, Me.State.AddingNewRow And False)
            End If

            Dim regionList As DropDownList = CType(moRegionGrid.Items(moRegionGrid.EditItemIndex).Cells(Me.GRID_COL_REGION).FindControl(Me.REGION_LIST_IN_GRID_CONTROL_NAME), DropDownList)
            'Invoke the RiskGroupFactoryLookup with NotingSelected = False
            'DEF-1692
            Dim myCountry As DropDownList = CType(moRegionGrid.Items(moRegionGrid.EditItemIndex).Cells(Me.GRID_COL_COUNTRY).FindControl(Me.COUNTRY_IN_GRID_CONTROL_NAME), DropDownList)
            PopulateRegionDropdown(regionList, New Guid(myCountry.SelectedItem.Value), True)
            'End of DEF-1692

            Me.TranslateGridControls(moRegionGrid)

            Me.SetButtonsState()
        End Sub

        Private Sub ReturnFromEditing(Optional ByVal cancel As Boolean = False)

            moRegionGrid.EditItemIndex = NO_ROW_SELECTED_INDEX

            If (Me.moRegionGrid.PageCount = 0) Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, moRegionGrid, False)
            Else
                ControlMgr.SetVisibleControl(Me, moRegionGrid, True)
            End If

            Me.State.IsEditMode = False
            Me.PopulateRegionGrid()
            Me.State.PageIndex = moRegionGrid.CurrentPageIndex
            SetButtonsState()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As DataGrid, ByVal cellPosition As Integer, ByVal itemIndex As Integer, ByVal IsNewRowAndSingleCountry As Boolean)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim ctrlCountry As DropDownList = CType(grid.Items(itemIndex).Cells(Me.GRID_COL_COUNTRY_ID).FindControl(Me.COUNTRY_IN_GRID_CONTROL_NAME), DropDownList)
            SetFocus(ctrlCountry)
            If cellPosition = Me.GRID_COL_COUNTRY_ID Then
                SetFocus(ctrlCountry)
                ctrlCountry.Enabled = True
            Else
                Dim ctrlDescription As TextBox = CType(grid.Items(itemIndex).Cells(Me.GRID_COL_REGION_ALIAS).FindControl(DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox)
                SetFocus(ctrlDescription)
                ctrlCountry.Enabled = False
            End If

            If IsNewRowAndSingleCountry Then
                ctrlCountry.SelectedIndex = FIRST_ROW_SELECTED_INDEX
                ctrlCountry.Enabled = False
            End If
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()
            Me.BindBOPropertyToGridHeader(Me.State.RegionAliasBO, "description", Me.moRegionGrid.Columns(Me.GRID_COL_REGION_ALIAS))
            Me.BindBOPropertyToGridHeader(Me.State.RegionAliasBO, "CountryId", Me.moRegionGrid.Columns(Me.GRID_COL_COUNTRY))
            Me.BindBOPropertyToGridHeader(Me.State.RegionAliasBO, "RegionId", Me.moRegionGrid.Columns(Me.GRID_COL_REGION))
            Me.ClearGridHeadersAndLabelsErrSign()
        End Sub

#End Region

#Region "Private Methods"

        Private Sub SetButtonsState()

            If (Me.State.IsEditMode) Then
                ControlMgr.SetVisibleControl(Me, Me.moBtnSave_WRITE, True)
                ControlMgr.SetVisibleControl(Me, moBtnCancel, True)
                ControlMgr.SetVisibleControl(Me, moBtnAdd_WRITE, False)
                ControlMgr.SetEnableControl(Me, moBtnSearch, False)
                ControlMgr.SetEnableControl(Me, moBtnClearSearch, False)
                ControlMgr.SetEnableControl(Me, moDropdownRegion, False)
                Me.MenuEnabled = False
                If (Me.cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, False)
                End If
            Else
                ControlMgr.SetVisibleControl(Me, moBtnSave_WRITE, False)
                ControlMgr.SetVisibleControl(Me, moBtnCancel, False)
                ControlMgr.SetVisibleControl(Me, moBtnAdd_WRITE, True)
                ControlMgr.SetEnableControl(Me, moBtnSearch, True)
                ControlMgr.SetEnableControl(Me, moBtnClearSearch, True)
                ControlMgr.SetEnableControl(Me, moDropdownRegion, True)
                Me.MenuEnabled = True
                If (Me.cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, True)
                End If
            End If

        End Sub
        Private Sub moBtnDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles moBtnDelete.Click
            Try
                'Place code here
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try
        End Sub

        Private Sub moBtnDelete_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles moBtnDelete.Command
            Try
                'Place code here
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try
        End Sub

        Private Sub PopulateCountry(CountryDropDown As DropDownList)
            'Me.BindListControlToDataView(moCountryDrop, LookupListNew.GetUserCountriesLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies))

            Dim CountryList As DataElements.ListItem() =
                            CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.Country)

            Dim UserCountries As DataElements.ListItem() = (From Country In CountryList
                                                            Where ElitaPlusIdentity.Current.ActiveUser.Countries.Contains(Country.ListItemId)
                                                            Select Country).ToArray()

            CountryDropDown.Populate(UserCountries.ToArray(),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })

            If moCountryDrop.Items.Count < 3 Then
                ControlMgr.SetVisibleControl(Me, moCountryLabel, False)
                ControlMgr.SetVisibleControl(Me, moCountryDrop, False)
            End If
        End Sub
#End Region


#Region " Datagrid Related "

        Private Sub moRegionGrid_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moRegionGrid.PageIndexChanged
            Try
                If (Not (Me.State.IsEditMode)) Then
                    Me.State.PageIndex = e.NewPageIndex
                    Me.moRegionGrid.CurrentPageIndex = Me.State.PageIndex
                    Me.PopulateRegionGrid()
                    Me.moRegionGrid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try
        End Sub

        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                moRegionGrid.CurrentPageIndex = NewCurrentPageIndex(moRegionGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.PopulateRegionGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try
        End Sub

        Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles moRegionGrid.ItemDataBound
            Try
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

                If (itemType = ListItemType.Item OrElse
                    itemType = ListItemType.AlternatingItem OrElse
                    itemType = ListItemType.SelectedItem) Then

                    e.Item.Cells(Me.GRID_COL_REGION_ALIAS_ID).Text = GetGuidStringFromByteArray(CType(dvRow(RegionStandardization.RegionStandardizationSearchDV.COL_REGION_STANDARDIZATION_ID), Byte()))

                    e.Item.Cells(Me.GRID_COL_COUNTRY).Text = GetGuidStringFromByteArray(CType(dvRow(RegionStandardization.RegionStandardizationSearchDV.COL_COUNTRY_ID), Byte()))
                    e.Item.Cells(Me.GRID_COL_COUNTRY).Text = dvRow(RegionStandardization.RegionStandardizationSearchDV.COL_COUNTRY_NAME).ToString

                    e.Item.Cells(Me.GRID_COL_REGION_ALIAS).Text = dvRow(RegionStandardization.RegionStandardizationSearchDV.COL_REGION_ALIAS).ToString
                    e.Item.Cells(Me.GRID_COL_REGION).Text = dvRow(RegionStandardization.RegionStandardizationSearchDV.COL_REGION).ToString

                ElseIf (itemType = ListItemType.EditItem) Then
                    e.Item.Cells(Me.GRID_COL_REGION_ALIAS_ID).Text = GetGuidStringFromByteArray(CType(dvRow(RegionStandardization.RegionStandardizationSearchDV.COL_REGION_STANDARDIZATION_ID), Byte()))

                    'Me.BindListControlToDataView(CType(e.Item.Cells(Me.GRID_COL_COUNTRY).FindControl(COUNTRY_IN_GRID_CONTROL_NAME), DropDownList),LookupListNew.GetUserCountriesLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies))
                    PopulateCountry(CType(e.Item.Cells(Me.GRID_COL_COUNTRY).FindControl(COUNTRY_IN_GRID_CONTROL_NAME), DropDownList))

                    Me.SetSelectedItem(CType(e.Item.Cells(Me.GRID_COL_COUNTRY).FindControl(COUNTRY_IN_GRID_CONTROL_NAME), DropDownList), Me.State.RegionAliasBO.CountryId)

                    CType(e.Item.Cells(Me.GRID_COL_REGION_ALIAS).FindControl(DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox).Text = dvRow(RegionStandardization.RegionStandardizationSearchDV.COL_REGION_ALIAS).ToString

                    Dim regionList As DropDownList = CType(e.Item.Cells(Me.GRID_COL_REGION).FindControl(Me.REGION_LIST_IN_GRID_CONTROL_NAME), DropDownList)
                    'DEF-1692
                    Dim myCountry As DropDownList = CType(e.Item.Cells(Me.GRID_COL_COUNTRY).FindControl(Me.COUNTRY_IN_GRID_CONTROL_NAME), DropDownList)
                    myCountry.AutoPostBack = True
                    Me.PopulateRegionDropdown(regionList, New Guid(myCountry.SelectedItem.Value), True)
                    'End of DEF-1692
                    Me.SetSelectedItem(regionList, Me.State.RegionAliasBO.RegionId) 'New Guid(CType(dvRow(RegionStandardization.RegionStandardizationSearchDV.COL_REGION_STANDARDIZATION_ID), Byte())))

                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try
        End Sub

        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles moRegionGrid.SortCommand
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
                Me.State.RegionAliasId = Guid.Empty
                Me.State.PageIndex = 0

                Me.PopulateRegionGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try

        End Sub

        Public Sub ItemBound(ByVal source As Object, ByVal e As DataGridItemEventArgs) Handles moRegionGrid.ItemDataBound
            Try
                BaseItemBound(source, e)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try
        End Sub

        'The pencil or the trash icon was clicked
        Sub ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
            Try
                Dim index As Integer = e.Item.ItemIndex

                If (e.CommandName = Me.EDIT_COMMAND) Then

                    'Do the Edit here

                    'Set the IsEditMode flag to TRUE
                    Me.State.IsEditMode = True

                    Me.State.RegionAliasId = New Guid(Me.moRegionGrid.Items(e.Item.ItemIndex).Cells(Me.GRID_COL_REGION_ALIAS_ID).Text)

                    Me.State.RegionAliasBO = New RegionStandardization(Me.State.RegionAliasId)

                    Me.PopulateRegionGrid()

                    Me.State.PageIndex = moRegionGrid.CurrentPageIndex

                    Me.SetGridControls(Me.moRegionGrid, False)

                    'Set focus on the Description TextBox for the EditItemIndex row
                    If moCountryDrop.Items.Count < 3 Then
                        Me.SetFocusOnEditableFieldInGrid(Me.moRegionGrid, Me.GRID_COL_REGION_ALIAS, index, Me.State.AddingNewRow And True)
                    Else
                        Me.SetFocusOnEditableFieldInGrid(Me.moRegionGrid, Me.GRID_COL_COUNTRY_ID, index, Me.State.AddingNewRow And False)
                    End If

                    Me.SetButtonsState()

                ElseIf (e.CommandName = Me.DELETE_COMMAND) Then
                    'Do the delete here

                    'Save the RegionAliasId in the Session
                    moRegionGrid.SelectedIndex = Me.NO_ROW_SELECTED_INDEX
                    Me.State.RegionAliasId = New Guid(Me.moRegionGrid.Items(e.Item.ItemIndex).Cells(Me.GRID_COL_REGION_ALIAS_ID).Text)
                    Me.State.RegionAliasBO = New RegionStandardization(Me.State.RegionAliasId)

                    Me.State.RegionAliasBO.Delete()

                    'Call the Save() method in the RegionAlias Business Object here

                    Me.State.RegionAliasBO.Save()

                    Me.State.PageIndex = moRegionGrid.CurrentPageIndex

                    'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
                    Me.State.IsAfterSave = True
                    Me.State.searchDV = Nothing
                    PopulateRegionGrid()
                    Me.State.PageIndex = moRegionGrid.CurrentPageIndex

                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try

        End Sub

        Public Sub ItemCreated(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub
#End Region

        'DEF-1692
        Protected Sub moCountryDrop_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles moCountryDrop.SelectedIndexChanged
            Dim selectCountry As String
            selectCountry = moCountryDrop.SelectedItem.Value
            PopulateRegionDropdown(moDropdownRegion, New Guid(selectCountry), True)
        End Sub
        Protected Sub cboCountryInGrid_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)

            Dim regionList As DropDownList = CType(moRegionGrid.Items(moRegionGrid.EditItemIndex).Cells(Me.GRID_COL_REGION).FindControl(Me.REGION_LIST_IN_GRID_CONTROL_NAME), DropDownList)
            Dim myCountry As DropDownList = CType(moRegionGrid.Items(moRegionGrid.EditItemIndex).Cells(Me.GRID_COL_COUNTRY).FindControl(Me.COUNTRY_IN_GRID_CONTROL_NAME), DropDownList)
            PopulateRegionDropdown(regionList, New Guid(myCountry.SelectedItem.Value), True)
        End Sub
        'End of DEF-1692

    End Class

End Namespace
