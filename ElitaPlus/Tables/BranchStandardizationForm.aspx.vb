Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Security.Cryptography.X509Certificates
Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Tables

    Partial Public Class BranchStandardizationForm
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
            Public BranchAliasBO As BranchStandardization
            Public BranchAliasId As Guid
            Public DealerId As Guid
            Public LangId As Guid
            'Public DealerId As Guid
            Public DescriptionMask As String = ""
            Public BranchIdSearch As Guid
            Public CompanyId As Guid
            Public SearchDealerId As Guid
            Public IsAfterSave As Boolean
            Public IsEditMode As Boolean
            Public IsGridVisible As Boolean
            Public dealerCount As Integer
            Public searchDV As BranchStandardization.BranchStandardizationSearchDV = Nothing
            Public SortExpression As String = BranchStandardization.BranchStandardizationSearchDV.COL_BRANCH_ALIAS
            'Public SortExpression As String = BranchStandardization.COL_NAME_DESCRIPTION
            Public AddingNewRow As Boolean
            Public Canceling As Boolean
            Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
            Public bnoRow As Boolean = False
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_

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
                IsEditing = (Me.Grid.EditIndex > NO_ROW_SELECTED_INDEX)
            End Get
        End Property

        Public ReadOnly Property DealerMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moDealerMultipleDrop Is Nothing Then
                    moDealerMultipleDrop = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return moDealerMultipleDrop
            End Get
        End Property

        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(ByVal value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

#End Region

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
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

        Public Const PAGETITLE As String = "BRANCH_STANDARDIZATION"
        Public Const PAGETAB As String = "TABLES"
        Private Const GRID_COL_BRANCH_ALIAS_ID As Integer = 2
        ' Private Const GRID_COL_DEALER_ID As Integer = 3
        Private Const GRID_COL_DEALER As Integer = 3
        Private Const GRID_COL_BRANCH_ALIAS As Integer = 4
        Private Const GRID_COL_BRANCH_ID As Integer = 5

        Private Const DBBRANCHALIAS_ID As Integer = 0
        Private Const DBCOUNTRY_ID As Integer = 1
        Private Const DBBRANCHALIAS As Integer = 2
        Private Const DBBRANCH As Integer = 3

        Private Const EDITIMG_COL As Integer = 0
        Private Const DELETEIMG_COL As Integer = 1
        Private Const BRANCHALIAS_ID_COL As Integer = 2
        Private Const COUNTRY_ID_COL As Integer = 3
        Private Const BRANCHALIAS_COL As Integer = 4
        Private Const BRANCH_COL As Integer = 5

        Private Const BRANCHALIAS_CONTROL_NAME As String = "moBranchstandardizationIdLabel"
        Private Const DEALER_IN_GRID_CONTROL_NAME_LABEL As String = "moDealerNameLabelGrid"
        Private Const DEALER_BRANCH_IN_GRID_CONTROL_NAME_LABEL As String = "moDealerBranchCodeLabelGrid"
        Private Const BRANCH_IN_GRID_CONTROL_NAME_LABEL As String = "moAsssurantBranchCodeLabelGrid"

        Private Const DEALER_IN_GRID_CONTROL_NAME As String = "moDealerNameDropGrid"
        Private Const DESCRIPTION_IN_GRID_CONTROL_NAME As String = "moDealerBranchCodeTextGrid"
        Private Const BRANCH_LIST_IN_GRID_CONTROL_NAME As String = "moAsssurantBranchCodeDropGrid"

        Private Const BRANCH_ALIAS_ID_IN_GRID_CONTROL_NAME As String = "moBranchAliasId"

        Private Const BRANCHALIASID_CONTROL_NUM As Integer = 1
        Private Const COUNTRYID_CONTROL_NUM As Integer = 1
        Private Const BRANCHALIAS_LABEL_CONTROL_NUM As Integer = 1
        Private Const BRANCHALIAS_TEXT_CONTROL_NUM As Integer = 3
        Private Const BRANCH_LABEL_CONTROL_NUM As Integer = 1
        Private Const BRANCH_DROP_DOWN_CONTROL_NUM As Integer = 3
        'Private Const BRANCHALIAS_CONTROL_NAME As String = "moBranchAliasId"

        Private Const PAGE_SIZE As Integer = 10

        Private Const DATAGRID_EDIT_BUTTON_NAME As String = "EditButton_WRITE"
        Private Const DATAGRID_DELETE_BUTTON_NAME As String = "DeleteButton_WRITE"
        Private Const EDIT_COMMAND As String = "EditRecord"
        Private Const DELETE_COMMAND As String = "DeleteRecord"
        Private Const MGS_CONFIRM_PROMPT As String = "MGS_CONFIRM_PROMPT" '"Are you sure you want to delete the selected records?"
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED" '"The record has not been saved because the current record has not been changed"
        Private Const LABEL_SELECT_DEALERCODE As String = "DEALER"

        Private Const NO_ROW_SELECTED_INDEX As Integer = -1
        Private Const FIRST_ROW_SELECTED_INDEX As Integer = 1

#End Region

#Region "Handlers"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                ErrControllerMaster.Clear_Hide()
                'Me.SetStateProperties()
                If Not Page.IsPostBack Then
                    Me.SortDirection = BranchStandardization.BranchStandardizationSearchDV.COL_DEALER_NAME
                    Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Me.SetDefaultButton(Me.moExternalBranchCodeText, Me.moBtnSearch)
                    Me.SetGridItemStyleColor(Me.Grid)
                    If Me.State.BranchAliasBO Is Nothing Then
                        Me.State.BranchAliasBO = New BranchStandardization
                    End If
                    Me.TranslateGridHeader(Grid)
                    Me.TranslateGridControls(Grid)
                    PopulateDealerDropdown()
                    Me.State.PageIndex = 0
                    SetButtonsState()
                    BindBoPropertiesToGridHeaders()
                Else
                    If Not Me.State.searchDV Is Nothing Then
                        Grid.DataSource = Me.State.searchDV
                    End If
                End If

                BindBoPropertiesToGridHeaders()
                CheckIfComingFromDeleteConfirm()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
            Me.ShowMissingTranslations(ErrControllerMaster)
        End Sub

        Private Sub moBtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnSearch.Click
            Try
                Me.State.IsGridVisible = True
                Me.State.searchDV = Nothing
                PopulateBranchGrid()
                Me.State.PageIndex = Grid.PageIndex
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub moBtnClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnClearSearch.Click
            Try
                Me.moExternalBranchCodeText.Text = String.Empty
                Me.DealerMultipleDrop.SelectedIndex = Me.BLANK_ITEM_SELECTED

                If (Not Me.moDropdownBranch.SelectedIndex.Equals(-1)) Then
                    Me.moDropdownBranch.SelectedIndex = Me.BLANK_ITEM_SELECTED
                End If

                'Update Page State
                With Me.State
                    .DescriptionMask = DealerMultipleDrop.TextColumnName
                    .BranchIdSearch = Nothing
                    .SearchDealerId = Nothing
                End With
                'Me.PopulateBranchGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub moBtnAdd_WITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnAdd_WRITE.Click

            Try
                If Me.State.dealerCount > 0 Then
                    Me.State.IsEditMode = True
                    Me.State.IsGridVisible = True
                    Me.State.AddingNewRow = True
                    Me.State.searchDV = Nothing
                    Me.AddBranchAlias()

                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub moBtnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnSave_WRITE.Click

            Try
                PopulateBOFromForm()
                If (Me.State.BranchAliasBO.IsDirty) Then
                    Me.State.BranchAliasBO.Save()
                    Me.State.IsAfterSave = True
                    Me.State.AddingNewRow = False
                    ShowInfoMsgBox(Me.MSG_RECORD_SAVED_OK)
                    Me.State.searchDV = Nothing
                    Me.ReturnFromEditing()
                Else
                    ShowInfoMsgBox(Me.MSG_RECORD_NOT_SAVED)
                    Me.ReturnFromEditing()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
                Me.ReturnFromEditing(True)
            End Try
        End Sub

        Private Sub moBtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnCancel.Click
            Try
                Me.Grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                Me.State.Canceling = True
                If (Me.State.AddingNewRow) Then
                    Me.State.AddingNewRow = False
                    Me.State.searchDV = Nothing
                End If
                ReturnFromEditing()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Protected Sub moDealerNameDropGrid_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

            ReloadNewFields()

        End Sub

        Protected Sub CheckIfComingFromDeleteConfirm()
            Dim confResponse As String = Me.HiddenDeletePromptResponse.Value
            If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    DoDelete()
                End If
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Delete
                End Select
            ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Delete
                End Select
            End If
            'Clean after consuming the action
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            'Me.HiddenDeletePromptResponse.Value = ""
        End Sub


        Private Sub DoDelete()
            'Do the delete here

            'Save the LanguageId in the Session

            Dim BranchStandardizationBO As BranchStandardization = New BranchStandardization(Me.State.BranchAliasId)

            BranchStandardizationBO.Delete()

            'Call the Save() method in the Language Business Object here

            BranchStandardizationBO.Save()

            'Me.State.PageIndex = Grid.PageIndex

            'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
            Me.State.IsAfterSave = True
            Me.State.searchDV = Nothing
            Me.PopulateBranchGrid()
            Me.State.PageIndex = Grid.PageIndex
            Me.State.IsEditMode = False
            Me.SetButtonsState()
        End Sub
#End Region

#Region "Button Management"

        Private Sub PopulateBranchGrid()


            'Dim dv As DataView
            Try
                If (Me.State.searchDV Is Nothing) Then
                    Me.SetStateProperties()
                    Me.State.searchDV = GetBranchGridDataView()
                End If

                Me.State.searchDV.Sort = Me.SortDirection

                If (Me.State.IsAfterSave) Then
                    Me.State.IsAfterSave = False
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.BranchAliasId, Me.Grid, Me.State.PageIndex)
                ElseIf (Me.State.IsEditMode) Then
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.BranchAliasId, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
                Else
                    'For a rare Delete scenario...
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.Grid, Me.State.PageIndex)
                End If

                Me.Grid.AutoGenerateColumns = False
                Me.Grid.Columns(Me.GRID_COL_DEALER).SortExpression = BranchStandardization.BranchStandardizationSearchDV.COL_DEALER_NAME
                Me.Grid.Columns(Me.GRID_COL_BRANCH_ALIAS).SortExpression = BranchStandardization.BranchStandardizationSearchDV.COL_BRANCH_ALIAS.ToUpper
                Me.Grid.Columns(Me.GRID_COL_BRANCH_ID).SortExpression = BranchStandardization.BranchStandardizationSearchDV.COL_BRANCH_CODE

                SortAndBindGrid()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub SortAndBindGrid()

            Me.State.PageIndex = Me.Grid.PageIndex

            If (Me.State.searchDV.Count = 0) Then
                Me.State.searchDV = Nothing
                Me.State.bnoRow = True
                Me.State.BranchAliasBO = New BranchStandardization
                Me.State.BranchAliasBO.AddNewRowToBranchStandardizationSearchDV(Me.State.searchDV, Me.State.BranchAliasBO)
                Me.Grid.DataSource = Me.State.searchDV
                Me.Grid.DataBind()
                Me.Grid.Rows(0).Visible = False
                Me.State.AddingNewRow = True
            Else
                Me.State.bnoRow = False
                Me.Grid.Enabled = True
                Grid.DataSource = Me.State.searchDV
                HighLightSortColumn(Grid, Me.State.SortExpression)
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

        Private Function GetBranchGridDataView() As BranchStandardization.BranchStandardizationSearchDV
            With State
                Me.State.searchDV = BranchStandardization.GetBranchAliasList(Me.State.DescriptionMask.ToUpper,
                                                                             Me.State.BranchIdSearch,
                                                                             Me.State.SearchDealerId)
            End With

            Me.State.searchDV.Sort = Grid.DataMember()
            Grid.DataSource = Me.State.searchDV

            Return (Me.State.searchDV)
        End Function

        Private Sub SetStateProperties()
            Dim company As New ElitaPlus.BusinessObjectsNew.Company(ElitaPlusIdentity.Current.ActiveUser.CompanyId)

            Me.State.DescriptionMask = moExternalBranchCodeText.Text
            'If (Not moDropdownDealerName.SelectedItem Is Nothing AndAlso moDropdownDealerName.SelectedItem.Value <> Me.NOTHING_SELECTED_TEXT) Then
            '    Me.State.SearchDealerId = Me.GetGuidFromString(moDropdownDealerName.SelectedItem.Value)
            'Else

            Me.State.SearchDealerId = Me.DealerMultipleDrop.SelectedGuid
            'End If

            If (Not moDropdownBranch.SelectedItem Is Nothing AndAlso moDropdownBranch.SelectedItem.Value <> Me.NOTHING_SELECTED_TEXT) Then
                Me.State.BranchIdSearch = Me.GetGuidFromString(moDropdownBranch.SelectedItem.Value)
            Else
                Me.State.BranchIdSearch = Nothing
            End If

            Me.State.CompanyId = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID

        End Sub

        Private Sub PopulateBOFromForm()
            Try
                With Me.State.BranchAliasBO
                    .DealerId = Me.GetSelectedItem(CType(Grid.Rows(Grid.EditIndex).Cells(Me.GRID_COL_DEALER).FindControl(DEALER_IN_GRID_CONTROL_NAME), DropDownList))
                    .DealerBranchCode = CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.GRID_COL_BRANCH_ALIAS).FindControl(Me.DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox).Text.ToUpper
                    .BranchId = Me.GetSelectedItem(CType(Grid.Rows(Grid.EditIndex).Cells(Me.GRID_COL_BRANCH_ID).FindControl(Me.BRANCH_LIST_IN_GRID_CONTROL_NAME), DropDownList))
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub PopulateDealerDropdown()
            Try
                Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, , , , True)
                Me.State.dealerCount = dv.Count

                DealerMultipleDrop.Caption = TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALERCODE)
                DealerMultipleDrop.NothingSelected = True
                DealerMultipleDrop.BindData(dv)
                DealerMultipleDrop.AutoPostBackDD = True

            Catch ex As Exception
            End Try

        End Sub

        Private Sub PopulateBranchDropdownGrid(ByVal oDealerDropDownList As DropDownList, ByVal oBranchDropDownList As DropDownList, Optional ByVal edmode As Boolean = False, Optional ByVal isNew As Boolean = False)
            Try
                Dim oDealerId As Guid = Me.GetSelectedItem(oDealerDropDownList)
                'Me.BindListControlToDataView(oBranchDropDownList,
                'LookupListNew.GetBranchCodeLookupList(oDealerId), "CODE", "ID", True) 'Dll
                Dim listcontext As ListContext = New ListContext()
                listcontext.DealerId = oDealerId
                Dim branchLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.BranchCodeByDealer, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                oBranchDropDownList.Populate(branchLkl, New PopulateOptions() With
                 {
                .AddBlankItem = True,
                .TextFunc = AddressOf .GetCode,
                .SortFunc = AddressOf .GetCode
                 })


            Catch ex As Exception
            End Try
        End Sub


        Private Sub AddBranchAlias()
            Me.State.searchDV = GetBranchGridDataView()


            Me.State.BranchAliasBO = New BranchStandardization
            Me.State.BranchAliasId = Me.State.BranchAliasBO.Id

            GetNewDataViewRow(Me.State.searchDV, Me.State.BranchAliasId)

            Grid.DataSource = Me.State.searchDV
            Me.SetGridControls(Me.Grid, False)

            Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.BranchAliasId, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)

            Me.Grid.AutoGenerateColumns = False
            Me.Grid.Columns(Me.GRID_COL_DEALER).SortExpression = BranchStandardization.BranchStandardizationSearchDV.COL_DEALER_NAME
            Me.Grid.Columns(Me.GRID_COL_BRANCH_ALIAS).SortExpression = BranchStandardization.BranchStandardizationSearchDV.COL_BRANCH_ALIAS
            Me.Grid.Columns(Me.GRID_COL_BRANCH_ID).SortExpression = BranchStandardization.BranchStandardizationSearchDV.COL_BRANCH_CODE


            Me.SortAndBindGrid()

            'Set focus on the Description TextBox for the EditItemIndex row
            Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.GRID_COL_DEALER, Grid.EditIndex)

            Dim ctrlDealer As DropDownList = CType(Grid.Rows(Grid.EditIndex).Cells(Me.GRID_COL_DEALER).FindControl(Me.DEALER_IN_GRID_CONTROL_NAME), DropDownList)
            Dim BranchList As DropDownList = CType(Grid.Rows(Grid.EditIndex).Cells(Me.GRID_COL_BRANCH_ID).FindControl(Me.BRANCH_LIST_IN_GRID_CONTROL_NAME), DropDownList)
            'Invoke the RiskGroupFactoryLookup with NotingSelected = False
            PopulateBranchDropdownGrid(ctrlDealer, BranchList, False)

            'Me.TranslateGridControls(Grid)

            Me.SetButtonsState()

        End Sub
        Private Sub ReloadNewFields()
            Dim oDealerDrop, oBatnchCodeDrop As DropDownList
            Dim oDealerBranchCode As TextBox
            Try
                oDealerBranchCode = CType(Grid.Rows(Grid.EditIndex).Cells(Me.GRID_COL_BRANCH_ALIAS).FindControl(Me.DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox)
                oDealerDrop = CType(Me.GetSelectedGridControl(Grid, Me.GRID_COL_DEALER), DropDownList)
                'oBatnchCodeDrop = CType(Me.GetSelectedGridControl(Grid, Me.GRID_COL_BRANCH_ID), DropDownList)
                If Not Me.State.searchDV Is Nothing Then
                    Dim oDealerId As Guid = Me.GetSelectedItem(oDealerDrop)
                    Grid.DataSource = Me.State.searchDV
                    Me.Grid.DataBind()
                    oDealerDrop = CType(Me.GetSelectedGridControl(Grid, Me.GRID_COL_DEALER), DropDownList)
                    oBatnchCodeDrop = CType(Me.GetSelectedGridControl(Grid, Me.GRID_COL_BRANCH_ID), DropDownList)
                    Me.SetSelectedItem(oDealerDrop, oDealerId)
                    PopulateBranchDropdownGrid(oDealerDrop, oBatnchCodeDrop)
                    CType(Grid.Rows(Grid.EditIndex).Cells(Me.GRID_COL_BRANCH_ALIAS).FindControl(Me.DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox).Text = oDealerBranchCode.Text
                End If
                SetButtonsState()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub ReturnFromEditing(Optional ByVal isSaveWithErrors As Boolean = False)

            If Not isSaveWithErrors Then

                Grid.EditIndex = NO_ROW_SELECTED_INDEX

                If (Me.Grid.PageCount = 0) Then
                    'if returning to the "1st time in" screen
                    ControlMgr.SetVisibleControl(Me, Grid, False)
                Else
                    ControlMgr.SetVisibleControl(Me, Grid, True)
                End If

                Me.State.IsEditMode = False
                Me.PopulateBranchGrid()
                Me.State.PageIndex = Grid.PageIndex
                SetButtonsState()
            Else
                ReloadNewFields()
            End If
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As GridView, ByVal cellPosition As Integer, ByVal itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim ind As Integer = grid.EditIndex
            Dim ctrlDealer As DropDownList = CType(grid.Rows(ind).Cells(Me.GRID_COL_DEALER).FindControl(Me.DEALER_IN_GRID_CONTROL_NAME), DropDownList)
            'Dim ctrlDealer As DropDownList = CType(grid.Rows(itemIndex).Cells(Me.GRID_COL_DEALER).FindControl(Me.DEALER_IN_GRID_CONTROL_NAME), DropDownList)

            SetFocus(ctrlDealer)

        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()
            Me.BindBOPropertyToGridHeader(Me.State.BranchAliasBO, "DealerBranchCode", Me.Grid.Columns(Me.GRID_COL_BRANCH_ALIAS))
            Me.BindBOPropertyToGridHeader(Me.State.BranchAliasBO, "dealerId", Me.Grid.Columns(Me.GRID_COL_DEALER))
            Me.BindBOPropertyToGridHeader(Me.State.BranchAliasBO, "BranchId", Me.Grid.Columns(Me.GRID_COL_BRANCH_ID))
            Me.ClearGridViewHeadersAndLabelsErrSign()
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
                ControlMgr.SetEnableControl(Me, moDropdownBranch, False)
                ControlMgr.SetEnableControl(Me, moExternalBranchCodeText, False)
                DealerMultipleDrop.ChangeEnabledControlProperty(False)
                Me.MenuEnabled = False
                If (Me.cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, False)
                End If
                Me.SetGridControls(Me.Grid, False)
            Else
                ControlMgr.SetVisibleControl(Me, moBtnSave_WRITE, False)
                ControlMgr.SetVisibleControl(Me, moBtnCancel, False)
                ControlMgr.SetVisibleControl(Me, moBtnAdd_WRITE, True)
                ControlMgr.SetEnableControl(Me, moBtnSearch, True)
                ControlMgr.SetEnableControl(Me, moBtnClearSearch, True)
                ControlMgr.SetEnableControl(Me, moDropdownBranch, True)
                ControlMgr.SetEnableControl(Me, moExternalBranchCodeText, True)
                DealerMultipleDrop.ChangeEnabledControlProperty(True)
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
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub moBtnDelete_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles moBtnDelete.Command
            Try
                'Place code here
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

#End Region

#Region " Datagrid Related "
        'Public Property SortDirection() As String
        '    Get
        '        Return ViewState("SortDirection").ToString
        '    End Get
        '    Set(ByVal value As String)
        '        ViewState("SortDirection") = value
        '    End Set
        'End Property

        Private Sub Grid_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                If (Not (Me.State.IsEditMode)) Then
                    Me.State.PageIndex = e.NewPageIndex
                    Me.Grid.PageIndex = Me.State.PageIndex
                    Me.PopulateBranchGrid()
                    Me.Grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.PopulateBranchGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                If Not dvRow Is Nothing And Not Me.State.bnoRow Then
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        CType(e.Row.Cells(Me.GRID_COL_BRANCH_ALIAS_ID).FindControl(Me.BRANCHALIAS_CONTROL_NAME), Label).Text = GetGuidStringFromByteArray(CType(dvRow(BranchStandardization.BranchStandardizationSearchDV.COL_BRANCH_STANDARDIZATION_ID), Byte()))

                        If (Me.State.IsEditMode = True _
                                AndAlso Me.State.BranchAliasId.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(BranchStandardization.BranchStandardizationSearchDV.COL_BRANCH_STANDARDIZATION_ID), Byte())))) Then

                            '   Me.BindListControlToDataView(CType(e.Row.Cells(Me.GRID_COL_DEALER).FindControl(DEALER_IN_GRID_CONTROL_NAME), DropDownList), LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, , , , True)) 'DealerWithEditBranchByUser

                            Dim listcontext As ListContext = New ListContext()
                            listcontext.UserId = ElitaPlusIdentity.Current.ActiveUser.Id
                            Dim listLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.DealerWithEditBranchByUser, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                            Dim dealerTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                                               Return li.Translation + "-" + li.Code
                                                                                           End Function
                            CType(e.Row.Cells(Me.GRID_COL_DEALER).FindControl(DEALER_IN_GRID_CONTROL_NAME), DropDownList).Populate(listLkl, New PopulateOptions() With
                             {
                              .AddBlankItem = True,
                              .TextFunc = dealerTextFunc,
                              .SortFunc = dealerTextFunc
                            })
                            Me.SetSelectedItem(CType(e.Row.Cells(Me.GRID_COL_DEALER).FindControl(DEALER_IN_GRID_CONTROL_NAME), DropDownList), Me.State.BranchAliasBO.DealerId)

                            Dim ctrlDealer As DropDownList = CType(e.Row.Cells(Me.GRID_COL_DEALER).FindControl(Me.DEALER_IN_GRID_CONTROL_NAME), DropDownList)
                            Dim BranchList As DropDownList = CType(e.Row.Cells(Me.GRID_COL_BRANCH_ID).FindControl(Me.BRANCH_LIST_IN_GRID_CONTROL_NAME), DropDownList)
                            Me.PopulateBranchDropdownGrid(ctrlDealer, BranchList, True)
                            Me.SetSelectedItem(BranchList, Me.State.BranchAliasBO.BranchId) 'New Guid(CType(dvRow(BranchStandardization.BranchStandardizationSearchDV.COL_Branch_STANDARDIZATION_ID), Byte())))

                            CType(e.Row.Cells(Me.GRID_COL_BRANCH_ALIAS).FindControl(Me.DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox).Text = dvRow(BranchStandardization.BranchStandardizationSearchDV.COL_BRANCH_ALIAS).ToString
                        Else
                            CType(e.Row.Cells(Me.GRID_COL_DEALER).FindControl(Me.DEALER_IN_GRID_CONTROL_NAME_LABEL), Label).Text = dvRow(BranchStandardization.BranchStandardizationSearchDV.COL_DEALER_NAME).ToString
                            CType(e.Row.Cells(Me.GRID_COL_BRANCH_ALIAS).FindControl(Me.DEALER_BRANCH_IN_GRID_CONTROL_NAME_LABEL), Label).Text = dvRow(BranchStandardization.BranchStandardizationSearchDV.COL_BRANCH_ALIAS).ToString
                            CType(e.Row.Cells(Me.GRID_COL_BRANCH_ID).FindControl(Me.BRANCH_IN_GRID_CONTROL_NAME_LABEL), Label).Text = dvRow(BranchStandardization.BranchStandardizationSearchDV.COL_BRANCH_CODE).ToString
                        End If
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub


        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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
                Me.State.BranchAliasId = Guid.Empty
                Me.State.PageIndex = 0

                Me.PopulateBranchGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Public Sub ItemBound(ByVal source As Object, ByVal e As GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                BaseItemBound(source, e)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        'The pencil or the trash icon was clicked
        Sub RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)



            Try

                Dim index As Integer


                If (e.CommandName = Me.EDIT_COMMAND) Then

                    'Do the Edit here
                    index = CInt(e.CommandArgument)
                    'Do the Edit here

                    'Set the IsEditMode flag to TRUE
                    Me.State.IsEditMode = True

                    Me.State.BranchAliasId = New Guid(CType(Me.Grid.Rows(index).Cells(Me.GRID_COL_BRANCH_ALIAS_ID).FindControl(Me.BRANCHALIAS_CONTROL_NAME), Label).Text)
                    Me.State.BranchAliasBO = New BranchStandardization(Me.State.BranchAliasId)

                    Me.PopulateBranchGrid()

                    Me.State.PageIndex = Grid.PageIndex

                    'Me.SetGridControls(Me.Grid, False)

                    'Set focus on the Code TextBox for the EditItemIndex row
                    Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.GRID_COL_DEALER, index)

                    Me.PopulateBranchGrid()

                    Me.SetButtonsState()

                ElseIf (e.CommandName = Me.DELETE_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    Me.State.BranchAliasId = New Guid(CType(Me.Grid.Rows(index).Cells(Me.GRID_COL_BRANCH_ALIAS_ID).FindControl(Me.BRANCHALIAS_CONTROL_NAME), Label).Text)
                    Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenDeletePromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
                If (e.CommandName = Me.DELETE_COMMAND) Then
                    Me.State.searchDV = Nothing
                    ReturnFromEditing()
                End If
            End Try

        End Sub

        Public Sub rowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub
#End Region

        Private Sub PopulateBranchCode()
            Try
                If Me.State.DealerId.Equals(Guid.Empty) Then
                    Return
                Else
                    ' Me.BindListControlToDataView(moDropdownBranch,
                    'LookupListNew.GetBranchCodeLookupList(Me.State.DealerId), "CODE", "ID", True) 'Dll
                    Dim listcontext As ListContext = New ListContext()
                    listcontext.DealerId = Me.State.DealerId
                    Dim branchLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.BranchCodeByDealer, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                    moDropdownBranch.Populate(branchLkl, New PopulateOptions() With
                     {
                    .AddBlankItem = True,
                    .TextFunc = AddressOf .GetCode,
                    .SortFunc = AddressOf .GetCode
                     })
                End If

                ControlMgr.SetEnableControl(Me, moDropdownBranch, True)
                Me.State.searchDV = Nothing
                PopulateBranchGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
            Handles moDealerMultipleDrop.SelectedDropChanged
            Try
                Me.State.DealerId = Me.DealerMultipleDrop.SelectedGuid()
                PopulateBranchCode()

            Catch ex As Exception
                HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub ShowInfoMsgBox(ByVal strMsg As String, Optional ByVal Translate As Boolean = True)
            Dim translatedMsg As String = strMsg
            If Translate Then translatedMsg = TranslationBase.TranslateLabelOrMessage(strMsg)
            Dim sJavaScript As String
            sJavaScript = "<SCRIPT>" & Environment.NewLine
            sJavaScript &= "setTimeout(""showMessage('" & translatedMsg & "', '" & "AlertWindow" & "', '" & Me.MSG_BTN_OK & "', '" & Me.MSG_TYPE_INFO & "', '" & "null" & "')"", 0);" & Environment.NewLine
            sJavaScript &= "</SCRIPT>" & Environment.NewLine
            Me.RegisterStartupScript("ShowConfirmation", sJavaScript)
        End Sub

    End Class

End Namespace