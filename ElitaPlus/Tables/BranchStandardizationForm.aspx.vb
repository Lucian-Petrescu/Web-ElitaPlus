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
                IsEditing = (Grid.EditIndex > NO_ROW_SELECTED_INDEX)
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
            Set(value As String)
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

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
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

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                ErrControllerMaster.Clear_Hide()
                'Me.SetStateProperties()
                If Not Page.IsPostBack Then
                    SortDirection = BranchStandardization.BranchStandardizationSearchDV.COL_DEALER_NAME
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    SetDefaultButton(moExternalBranchCodeText, moBtnSearch)
                    SetGridItemStyleColor(Grid)
                    If State.BranchAliasBO Is Nothing Then
                        State.BranchAliasBO = New BranchStandardization
                    End If
                    TranslateGridHeader(Grid)
                    TranslateGridControls(Grid)
                    PopulateDealerDropdown()
                    State.PageIndex = 0
                    SetButtonsState()
                    BindBoPropertiesToGridHeaders()
                Else
                    If State.searchDV IsNot Nothing Then
                        Grid.DataSource = State.searchDV
                    End If
                End If

                BindBoPropertiesToGridHeaders()
                CheckIfComingFromDeleteConfirm()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)
        End Sub

        Private Sub moBtnSearch_Click(sender As System.Object, e As System.EventArgs) Handles moBtnSearch.Click
            Try
                State.IsGridVisible = True
                State.searchDV = Nothing
                PopulateBranchGrid()
                State.PageIndex = Grid.PageIndex
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub moBtnClearSearch_Click(sender As System.Object, e As System.EventArgs) Handles moBtnClearSearch.Click
            Try
                moExternalBranchCodeText.Text = String.Empty
                DealerMultipleDrop.SelectedIndex = BLANK_ITEM_SELECTED

                If (Not moDropdownBranch.SelectedIndex.Equals(-1)) Then
                    moDropdownBranch.SelectedIndex = BLANK_ITEM_SELECTED
                End If

                'Update Page State
                With State
                    .DescriptionMask = DealerMultipleDrop.TextColumnName
                    .BranchIdSearch = Nothing
                    .SearchDealerId = Nothing
                End With
                'Me.PopulateBranchGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub moBtnAdd_WITE_Click(sender As System.Object, e As System.EventArgs) Handles moBtnAdd_WRITE.Click

            Try
                If State.dealerCount > 0 Then
                    State.IsEditMode = True
                    State.IsGridVisible = True
                    State.AddingNewRow = True
                    State.searchDV = Nothing
                    AddBranchAlias()

                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub moBtnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles moBtnSave_WRITE.Click

            Try
                PopulateBOFromForm()
                If (State.BranchAliasBO.IsDirty) Then
                    State.BranchAliasBO.Save()
                    State.IsAfterSave = True
                    State.AddingNewRow = False
                    ShowInfoMsgBox(MSG_RECORD_SAVED_OK)
                    State.searchDV = Nothing
                    ReturnFromEditing()
                Else
                    ShowInfoMsgBox(MSG_RECORD_NOT_SAVED)
                    ReturnFromEditing()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
                ReturnFromEditing(True)
            End Try
        End Sub

        Private Sub moBtnCancel_Click(sender As System.Object, e As System.EventArgs) Handles moBtnCancel.Click
            Try
                Grid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                State.Canceling = True
                If (State.AddingNewRow) Then
                    State.AddingNewRow = False
                    State.searchDV = Nothing
                End If
                ReturnFromEditing()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Protected Sub moDealerNameDropGrid_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)

            ReloadNewFields()

        End Sub

        Protected Sub CheckIfComingFromDeleteConfirm()
            Dim confResponse As String = HiddenDeletePromptResponse.Value
            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    DoDelete()
                End If
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Delete
                End Select
            ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Delete
                End Select
            End If
            'Clean after consuming the action
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            'Me.HiddenDeletePromptResponse.Value = ""
        End Sub


        Private Sub DoDelete()
            'Do the delete here

            'Save the LanguageId in the Session

            Dim BranchStandardizationBO As BranchStandardization = New BranchStandardization(State.BranchAliasId)

            BranchStandardizationBO.Delete()

            'Call the Save() method in the Language Business Object here

            BranchStandardizationBO.Save()

            'Me.State.PageIndex = Grid.PageIndex

            'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
            State.IsAfterSave = True
            State.searchDV = Nothing
            PopulateBranchGrid()
            State.PageIndex = Grid.PageIndex
            State.IsEditMode = False
            SetButtonsState()
        End Sub
#End Region

#Region "Button Management"

        Private Sub PopulateBranchGrid()


            'Dim dv As DataView
            Try
                If (State.searchDV Is Nothing) Then
                    SetStateProperties()
                    State.searchDV = GetBranchGridDataView()
                End If

                State.searchDV.Sort = SortDirection

                If (State.IsAfterSave) Then
                    State.IsAfterSave = False
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.BranchAliasId, Grid, State.PageIndex)
                ElseIf (State.IsEditMode) Then
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.BranchAliasId, Grid, State.PageIndex, State.IsEditMode)
                Else
                    'For a rare Delete scenario...
                    SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, Grid, State.PageIndex)
                End If

                Grid.AutoGenerateColumns = False
                Grid.Columns(GRID_COL_DEALER).SortExpression = BranchStandardization.BranchStandardizationSearchDV.COL_DEALER_NAME
                Grid.Columns(GRID_COL_BRANCH_ALIAS).SortExpression = BranchStandardization.BranchStandardizationSearchDV.COL_BRANCH_ALIAS.ToUpper
                Grid.Columns(GRID_COL_BRANCH_ID).SortExpression = BranchStandardization.BranchStandardizationSearchDV.COL_BRANCH_CODE

                SortAndBindGrid()

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub SortAndBindGrid()

            State.PageIndex = Grid.PageIndex

            If (State.searchDV.Count = 0) Then
                State.searchDV = Nothing
                State.bnoRow = True
                State.BranchAliasBO = New BranchStandardization
                State.BranchAliasBO.AddNewRowToBranchStandardizationSearchDV(State.searchDV, State.BranchAliasBO)
                Grid.DataSource = State.searchDV
                Grid.DataBind()
                Grid.Rows(0).Visible = False
                State.AddingNewRow = True
            Else
                State.bnoRow = False
                Grid.Enabled = True
                Grid.DataSource = State.searchDV
                HighLightSortColumn(Grid, State.SortExpression)
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

        Private Function GetBranchGridDataView() As BranchStandardization.BranchStandardizationSearchDV
            With State
                State.searchDV = BranchStandardization.GetBranchAliasList(State.DescriptionMask.ToUpper,
                                                                             State.BranchIdSearch,
                                                                             State.SearchDealerId)
            End With

            State.searchDV.Sort = Grid.DataMember()
            Grid.DataSource = State.searchDV

            Return (State.searchDV)
        End Function

        Private Sub SetStateProperties()
            Dim company As New ElitaPlus.BusinessObjectsNew.Company(ElitaPlusIdentity.Current.ActiveUser.CompanyId)

            State.DescriptionMask = moExternalBranchCodeText.Text
            'If (Not moDropdownDealerName.SelectedItem Is Nothing AndAlso moDropdownDealerName.SelectedItem.Value <> Me.NOTHING_SELECTED_TEXT) Then
            '    Me.State.SearchDealerId = Me.GetGuidFromString(moDropdownDealerName.SelectedItem.Value)
            'Else

            State.SearchDealerId = DealerMultipleDrop.SelectedGuid
            'End If

            If (moDropdownBranch.SelectedItem IsNot Nothing AndAlso moDropdownBranch.SelectedItem.Value <> NOTHING_SELECTED_TEXT) Then
                State.BranchIdSearch = GetGuidFromString(moDropdownBranch.SelectedItem.Value)
            Else
                State.BranchIdSearch = Nothing
            End If

            State.CompanyId = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID

        End Sub

        Private Sub PopulateBOFromForm()
            Try
                With State.BranchAliasBO
                    .DealerId = GetSelectedItem(CType(Grid.Rows(Grid.EditIndex).Cells(GRID_COL_DEALER).FindControl(DEALER_IN_GRID_CONTROL_NAME), DropDownList))
                    .DealerBranchCode = CType(Grid.Rows(Grid.EditIndex).Cells(GRID_COL_BRANCH_ALIAS).FindControl(DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox).Text.ToUpper
                    .BranchId = GetSelectedItem(CType(Grid.Rows(Grid.EditIndex).Cells(GRID_COL_BRANCH_ID).FindControl(BRANCH_LIST_IN_GRID_CONTROL_NAME), DropDownList))
                End With
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub PopulateDealerDropdown()
            Try
                Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, , , , True)
                State.dealerCount = dv.Count

                DealerMultipleDrop.Caption = TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALERCODE)
                DealerMultipleDrop.NothingSelected = True
                DealerMultipleDrop.BindData(dv)
                DealerMultipleDrop.AutoPostBackDD = True

            Catch ex As Exception
            End Try

        End Sub

        Private Sub PopulateBranchDropdownGrid(oDealerDropDownList As DropDownList, oBranchDropDownList As DropDownList, Optional ByVal edmode As Boolean = False, Optional ByVal isNew As Boolean = False)
            Try
                Dim oDealerId As Guid = GetSelectedItem(oDealerDropDownList)
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
            State.searchDV = GetBranchGridDataView()


            State.BranchAliasBO = New BranchStandardization
            State.BranchAliasId = State.BranchAliasBO.Id

            GetNewDataViewRow(State.searchDV, State.BranchAliasId)

            Grid.DataSource = State.searchDV
            SetGridControls(Grid, False)

            SetPageAndSelectedIndexFromGuid(State.searchDV, State.BranchAliasId, Grid, State.PageIndex, State.IsEditMode)

            Grid.AutoGenerateColumns = False
            Grid.Columns(GRID_COL_DEALER).SortExpression = BranchStandardization.BranchStandardizationSearchDV.COL_DEALER_NAME
            Grid.Columns(GRID_COL_BRANCH_ALIAS).SortExpression = BranchStandardization.BranchStandardizationSearchDV.COL_BRANCH_ALIAS
            Grid.Columns(GRID_COL_BRANCH_ID).SortExpression = BranchStandardization.BranchStandardizationSearchDV.COL_BRANCH_CODE


            SortAndBindGrid()

            'Set focus on the Description TextBox for the EditItemIndex row
            SetFocusOnEditableFieldInGrid(Grid, GRID_COL_DEALER, Grid.EditIndex)

            Dim ctrlDealer As DropDownList = CType(Grid.Rows(Grid.EditIndex).Cells(GRID_COL_DEALER).FindControl(DEALER_IN_GRID_CONTROL_NAME), DropDownList)
            Dim BranchList As DropDownList = CType(Grid.Rows(Grid.EditIndex).Cells(GRID_COL_BRANCH_ID).FindControl(BRANCH_LIST_IN_GRID_CONTROL_NAME), DropDownList)
            'Invoke the RiskGroupFactoryLookup with NotingSelected = False
            PopulateBranchDropdownGrid(ctrlDealer, BranchList, False)

            'Me.TranslateGridControls(Grid)

            SetButtonsState()

        End Sub
        Private Sub ReloadNewFields()
            Dim oDealerDrop, oBatnchCodeDrop As DropDownList
            Dim oDealerBranchCode As TextBox
            Try
                oDealerBranchCode = CType(Grid.Rows(Grid.EditIndex).Cells(GRID_COL_BRANCH_ALIAS).FindControl(DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox)
                oDealerDrop = CType(GetSelectedGridControl(Grid, GRID_COL_DEALER), DropDownList)
                'oBatnchCodeDrop = CType(Me.GetSelectedGridControl(Grid, Me.GRID_COL_BRANCH_ID), DropDownList)
                If State.searchDV IsNot Nothing Then
                    Dim oDealerId As Guid = GetSelectedItem(oDealerDrop)
                    Grid.DataSource = State.searchDV
                    Grid.DataBind()
                    oDealerDrop = CType(GetSelectedGridControl(Grid, GRID_COL_DEALER), DropDownList)
                    oBatnchCodeDrop = CType(GetSelectedGridControl(Grid, GRID_COL_BRANCH_ID), DropDownList)
                    SetSelectedItem(oDealerDrop, oDealerId)
                    PopulateBranchDropdownGrid(oDealerDrop, oBatnchCodeDrop)
                    CType(Grid.Rows(Grid.EditIndex).Cells(GRID_COL_BRANCH_ALIAS).FindControl(DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox).Text = oDealerBranchCode.Text
                End If
                SetButtonsState()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub ReturnFromEditing(Optional ByVal isSaveWithErrors As Boolean = False)

            If Not isSaveWithErrors Then

                Grid.EditIndex = NO_ROW_SELECTED_INDEX

                If (Grid.PageCount = 0) Then
                    'if returning to the "1st time in" screen
                    ControlMgr.SetVisibleControl(Me, Grid, False)
                Else
                    ControlMgr.SetVisibleControl(Me, Grid, True)
                End If

                State.IsEditMode = False
                PopulateBranchGrid()
                State.PageIndex = Grid.PageIndex
                SetButtonsState()
            Else
                ReloadNewFields()
            End If
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(grid As GridView, cellPosition As Integer, itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim ind As Integer = grid.EditIndex
            Dim ctrlDealer As DropDownList = CType(grid.Rows(ind).Cells(GRID_COL_DEALER).FindControl(DEALER_IN_GRID_CONTROL_NAME), DropDownList)
            'Dim ctrlDealer As DropDownList = CType(grid.Rows(itemIndex).Cells(Me.GRID_COL_DEALER).FindControl(Me.DEALER_IN_GRID_CONTROL_NAME), DropDownList)

            SetFocus(ctrlDealer)

        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()
            BindBOPropertyToGridHeader(State.BranchAliasBO, "DealerBranchCode", Grid.Columns(GRID_COL_BRANCH_ALIAS))
            BindBOPropertyToGridHeader(State.BranchAliasBO, "dealerId", Grid.Columns(GRID_COL_DEALER))
            BindBOPropertyToGridHeader(State.BranchAliasBO, "BranchId", Grid.Columns(GRID_COL_BRANCH_ID))
            ClearGridViewHeadersAndLabelsErrSign()
        End Sub

#End Region

#Region "Private Methods"

        Private Sub SetButtonsState()

            If (State.IsEditMode) Then
                ControlMgr.SetVisibleControl(Me, moBtnSave_WRITE, True)
                ControlMgr.SetVisibleControl(Me, moBtnCancel, True)
                ControlMgr.SetVisibleControl(Me, moBtnAdd_WRITE, False)
                ControlMgr.SetEnableControl(Me, moBtnSearch, False)
                ControlMgr.SetEnableControl(Me, moBtnClearSearch, False)
                ControlMgr.SetEnableControl(Me, moDropdownBranch, False)
                ControlMgr.SetEnableControl(Me, moExternalBranchCodeText, False)
                DealerMultipleDrop.ChangeEnabledControlProperty(False)
                MenuEnabled = False
                If (cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, False)
                End If
                SetGridControls(Grid, False)
            Else
                ControlMgr.SetVisibleControl(Me, moBtnSave_WRITE, False)
                ControlMgr.SetVisibleControl(Me, moBtnCancel, False)
                ControlMgr.SetVisibleControl(Me, moBtnAdd_WRITE, True)
                ControlMgr.SetEnableControl(Me, moBtnSearch, True)
                ControlMgr.SetEnableControl(Me, moBtnClearSearch, True)
                ControlMgr.SetEnableControl(Me, moDropdownBranch, True)
                ControlMgr.SetEnableControl(Me, moExternalBranchCodeText, True)
                DealerMultipleDrop.ChangeEnabledControlProperty(True)
                MenuEnabled = True
                If (cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, True)
                End If
            End If

        End Sub
        Private Sub moBtnDelete_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles moBtnDelete.Click
            Try
                'Place code here
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub moBtnDelete_Command(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs) Handles moBtnDelete.Command
            Try
                'Place code here
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
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

        Private Sub Grid_PageIndexChanged(source As System.Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                If (Not (State.IsEditMode)) Then
                    State.PageIndex = e.NewPageIndex
                    Grid.PageIndex = State.PageIndex
                    PopulateBranchGrid()
                    Grid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                PopulateBranchGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                If dvRow IsNot Nothing And Not State.bnoRow Then
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        CType(e.Row.Cells(GRID_COL_BRANCH_ALIAS_ID).FindControl(BRANCHALIAS_CONTROL_NAME), Label).Text = GetGuidStringFromByteArray(CType(dvRow(BranchStandardization.BranchStandardizationSearchDV.COL_BRANCH_STANDARDIZATION_ID), Byte()))

                        If (State.IsEditMode = True _
                                AndAlso State.BranchAliasId.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(BranchStandardization.BranchStandardizationSearchDV.COL_BRANCH_STANDARDIZATION_ID), Byte())))) Then

                            '   Me.BindListControlToDataView(CType(e.Row.Cells(Me.GRID_COL_DEALER).FindControl(DEALER_IN_GRID_CONTROL_NAME), DropDownList), LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, , , , True)) 'DealerWithEditBranchByUser

                            Dim listcontext As ListContext = New ListContext()
                            listcontext.UserId = ElitaPlusIdentity.Current.ActiveUser.Id
                            Dim listLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.DealerWithEditBranchByUser, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                            Dim dealerTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                                               Return li.Translation + "-" + li.Code
                                                                                           End Function
                            CType(e.Row.Cells(GRID_COL_DEALER).FindControl(DEALER_IN_GRID_CONTROL_NAME), DropDownList).Populate(listLkl, New PopulateOptions() With
                             {
                              .AddBlankItem = True,
                              .TextFunc = dealerTextFunc,
                              .SortFunc = dealerTextFunc
                            })
                            SetSelectedItem(CType(e.Row.Cells(GRID_COL_DEALER).FindControl(DEALER_IN_GRID_CONTROL_NAME), DropDownList), State.BranchAliasBO.DealerId)

                            Dim ctrlDealer As DropDownList = CType(e.Row.Cells(GRID_COL_DEALER).FindControl(DEALER_IN_GRID_CONTROL_NAME), DropDownList)
                            Dim BranchList As DropDownList = CType(e.Row.Cells(GRID_COL_BRANCH_ID).FindControl(BRANCH_LIST_IN_GRID_CONTROL_NAME), DropDownList)
                            PopulateBranchDropdownGrid(ctrlDealer, BranchList, True)
                            SetSelectedItem(BranchList, State.BranchAliasBO.BranchId) 'New Guid(CType(dvRow(BranchStandardization.BranchStandardizationSearchDV.COL_Branch_STANDARDIZATION_ID), Byte())))

                            CType(e.Row.Cells(GRID_COL_BRANCH_ALIAS).FindControl(DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox).Text = dvRow(BranchStandardization.BranchStandardizationSearchDV.COL_BRANCH_ALIAS).ToString
                        Else
                            CType(e.Row.Cells(GRID_COL_DEALER).FindControl(DEALER_IN_GRID_CONTROL_NAME_LABEL), Label).Text = dvRow(BranchStandardization.BranchStandardizationSearchDV.COL_DEALER_NAME).ToString
                            CType(e.Row.Cells(GRID_COL_BRANCH_ALIAS).FindControl(DEALER_BRANCH_IN_GRID_CONTROL_NAME_LABEL), Label).Text = dvRow(BranchStandardization.BranchStandardizationSearchDV.COL_BRANCH_ALIAS).ToString
                            CType(e.Row.Cells(GRID_COL_BRANCH_ID).FindControl(BRANCH_IN_GRID_CONTROL_NAME_LABEL), Label).Text = dvRow(BranchStandardization.BranchStandardizationSearchDV.COL_BRANCH_CODE).ToString
                        End If
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub


        Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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
                'Set the Me.State.selectedClaimId = Guid.Empty and set Me.State.PageIndex = 0
                State.BranchAliasId = Guid.Empty
                State.PageIndex = 0

                PopulateBranchGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Public Sub ItemBound(source As Object, e As GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                BaseItemBound(source, e)
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        'The pencil or the trash icon was clicked
        Sub RowCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)



            Try

                Dim index As Integer


                If (e.CommandName = EDIT_COMMAND) Then

                    'Do the Edit here
                    index = CInt(e.CommandArgument)
                    'Do the Edit here

                    'Set the IsEditMode flag to TRUE
                    State.IsEditMode = True

                    State.BranchAliasId = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_BRANCH_ALIAS_ID).FindControl(BRANCHALIAS_CONTROL_NAME), Label).Text)
                    State.BranchAliasBO = New BranchStandardization(State.BranchAliasId)

                    PopulateBranchGrid()

                    State.PageIndex = Grid.PageIndex

                    'Me.SetGridControls(Me.Grid, False)

                    'Set focus on the Code TextBox for the EditItemIndex row
                    SetFocusOnEditableFieldInGrid(Grid, GRID_COL_DEALER, index)

                    PopulateBranchGrid()

                    SetButtonsState()

                ElseIf (e.CommandName = DELETE_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    State.BranchAliasId = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_BRANCH_ALIAS_ID).FindControl(BRANCHALIAS_CONTROL_NAME), Label).Text)
                    DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenDeletePromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
                If (e.CommandName = DELETE_COMMAND) Then
                    State.searchDV = Nothing
                    ReturnFromEditing()
                End If
            End Try

        End Sub

        Public Sub rowCreated(sender As Object, e As GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub
#End Region

        Private Sub PopulateBranchCode()
            Try
                If State.DealerId.Equals(Guid.Empty) Then
                    Return
                Else
                    ' Me.BindListControlToDataView(moDropdownBranch,
                    'LookupListNew.GetBranchCodeLookupList(Me.State.DealerId), "CODE", "ID", True) 'Dll
                    Dim listcontext As ListContext = New ListContext()
                    listcontext.DealerId = State.DealerId
                    Dim branchLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.BranchCodeByDealer, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                    moDropdownBranch.Populate(branchLkl, New PopulateOptions() With
                     {
                    .AddBlankItem = True,
                    .TextFunc = AddressOf .GetCode,
                    .SortFunc = AddressOf .GetCode
                     })
                End If

                ControlMgr.SetEnableControl(Me, moDropdownBranch, True)
                State.searchDV = Nothing
                PopulateBranchGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub OnFromDrop_Changed(fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
            Handles moDealerMultipleDrop.SelectedDropChanged
            Try
                State.DealerId = DealerMultipleDrop.SelectedGuid()
                PopulateBranchCode()

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub ShowInfoMsgBox(strMsg As String, Optional ByVal Translate As Boolean = True)
            Dim translatedMsg As String = strMsg
            If Translate Then translatedMsg = TranslationBase.TranslateLabelOrMessage(strMsg)
            Dim sJavaScript As String
            sJavaScript = "<SCRIPT>" & Environment.NewLine
            sJavaScript &= "setTimeout(""showMessage('" & translatedMsg & "', '" & "AlertWindow" & "', '" & MSG_BTN_OK & "', '" & MSG_TYPE_INFO & "', '" & "null" & "')"", 0);" & Environment.NewLine
            sJavaScript &= "</SCRIPT>" & Environment.NewLine
            RegisterStartupScript("ShowConfirmation", sJavaScript)
        End Sub

    End Class

End Namespace