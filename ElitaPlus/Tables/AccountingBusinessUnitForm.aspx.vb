Option Strict On
Option Explicit On

Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Tables
    Partial Class AccountingBusinessUnitForm
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
                IsEditing = (Grid.EditIndex > NO_ROW_SELECTED_INDEX)
            End Get
        End Property

        Private WriteOnly Property EnableControls() As Boolean
            Set(Value As Boolean)
                ControlMgr.SetEnableControl(Me, SearchButton, Value)
                ControlMgr.SetEnableControl(Me, ClearButton, Value)
                ControlMgr.SetEnableControl(Me, NewButton_WRITE, Value)
            End Set
        End Property

#End Region

#Region "Page State"
        Class MyState
            Public PageIndex As Integer = 0
            '  Public MyBO As AcctBusinessUnit = New AcctBusinessUnit
            Public MyBO As AcctBusinessUnit
            Public MyAcctCompany As ArrayList = New ArrayList
            Public AcctCompanyIDMask As String
            Public AcctCompanyDescriptionMask As String
            Public BusinessUnitMask As String
            Public CompanyGroupId As Guid
            Public AcctBusinessUnitID As Guid   'Id As Guid
            Public IsGridVisible As Boolean
            Public IsAfterSave As Boolean
            Public IsEditMode As Boolean
            Public AddingNewRow As Boolean
            Public Canceling As Boolean
            Public searchDV As DataView = Nothing
            Public YESNOdv As DataView = Nothing
            Public editRowIndex As Integer
            'Public SortExpression As String = AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_BUSINESS_UNIT
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
        Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

        Private Const EDIT_COMMAND As String = "EditRecord"
        Private Const DELETE_COMMAND As String = "DeleteRecord"
        Private Const SORT_COMMAND As String = "Sort"

        Private Const NO_ROW_SELECTED_INDEX As Integer = -1

        Private Const ACCT_BUSINESS_UNIT_ID_COL As Integer = 2
        Private Const ACCT_COMPANY_ID_COL As Integer = 3
        Private Const BUSINESS_UNIT_COL As Integer = 4
        Private Const CODE_COL As Integer = 5
        Private Const ACCT_COMPANY_DESCRIPTION_COL As Integer = 6
        Private Const SUPPRESS_VEND_COL As Integer = 7
        Private Const PAYMENT_METHOD_COL As Integer = 8

        Private Const YESNO As String = "YESNO"

        Private Const ACCT_BUSINESS_UNIT_ID_LABEL As String = "lblAcctBusinessUnitID"
        Private Const BUSINESS_UNIT_LABEL As String = "lblColBusinessUnit"
        Private Const CODE_LABEL As String = "lblColCode"
        Private Const BUSINESS_UNIT_TEXTBOX As String = "txtColBusinessUnit"
        Private Const CODE_TEXTBOX As String = "txtColCode"
        Private Const ACCT_COMPANY_DESCRIPTION_LABEL As String = "lblColAcctCompanyDescription"
        Private Const ACCT_COMPANY_DROPLIST As String = "ddlstColAcctCompany"
        Private Const SUPPRESS_VEND_LABEL As String = "lblColSuppressVendors"
        Private Const SUPPRESS_VEND_DROPLIST As String = "ddlColSuppressVendors"
        Private Const PAYMENT_METHOD_LABEL As String = "lblColPaymentMethod"
        Private Const PAYMENT_METHOD_DROPLIST As String = "ddlColPaymentMethod"

        Public Const PAGETITLE As String = "ACCOUNTING_BUSINESS_UNIT"
        Public Const PAGETAB As String = "TABLES"
#End Region

#Region "Button Click Handlers"

        Private Sub SearchButton_Click(sender As System.Object, e As System.EventArgs) Handles SearchButton.Click
            Try
                State.IsGridVisible = True
                State.searchDV = Nothing
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub ClearButton_Click(sender As System.Object, e As System.EventArgs) Handles ClearButton.Click

            Try
                ClearSearchCriteria()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub ClearSearchCriteria()

            Try
                SearchBusinessUnitTextBox.Text = String.Empty
                SearchAcctCompanyDropdownList.SelectedIndex = BLANK_ITEM_SELECTED

                'Update Page State
                With State
                    .BusinessUnitMask = SearchBusinessUnitTextBox.Text
                    .AcctCompanyIDMask = String.Empty
                End With
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub NewButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles NewButton_WRITE.Click

            Try
                State.IsEditMode = True
                State.IsGridVisible = True
                State.AddingNewRow = True
                AddNew()
                SetButtonsState()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub SaveButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles SaveButton_WRITE.Click

            Try
                AssignBOFromSelectedRecord()
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
                HandleErrors(ex, ErrControllerMaster)
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
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

#End Region


#Region "Private Methods"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

            'Put user code to initialize the page here
            Try
                ErrControllerMaster.Clear_Hide()

                SetFormTitle(PAGETITLE)
                SetFormTab(PAGETAB)

                SetStateProperties()
                If Not Page.IsPostBack Then
                    SortDirection = AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_BUSINESS_UNIT
                    SetDefaultButton(SearchBusinessUnitTextBox, SearchButton)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    SetGridItemStyleColor(Grid)
                    State.PageIndex = 0
                    If State.MyBO Is Nothing Then
                        State.MyBO = New AcctBusinessUnit
                    End If
                    SetButtonsState()
                    TranslateGridHeader(Grid)
                    TranslateGridControls(Grid)
                    PopulateYesNo()
                    PopulateAcctCompanyDropdown(SearchAcctCompanyDropdownList)
                Else
                    CheckIfComingFromDeleteConfirm()
                End If
                BindBoPropertiesToGridHeaders()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)
        End Sub

        Private Sub PopulateAcctCompanyDropdown(listCtl As System.Web.UI.WebControls.DropDownList)
            If ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies.Length > 0 Then
                listCtl.Items.Add(New System.Web.UI.WebControls.ListItem("", Guid.Empty.ToString))
                State.MyAcctCompany.Add(Guid.Empty)
                For Each _acctCo As AcctCompany In ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies
                    If Not _acctCo.IsNew Then
                        listCtl.Items.Add(New System.Web.UI.WebControls.ListItem(_acctCo.Description, _acctCo.Id.ToString))
                        State.MyAcctCompany.Add(_acctCo.Id)
                    Else
                        ErrControllerMaster.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_ACCOUNTING_COMPANIES_NOT_FOUND_ERR)
                        EnableControls = False
                        Exit Sub
                    End If
                Next
            End If
            'Me.BindListControlToDataView(dlistAcctCompany, LookupListNew.GetAcctCompanyLookupList())
        End Sub

        Private Sub PopulateYesNo()
            Dim YESNOdv As DataView = LookupListNew.DropdownLookupList(YESNO, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
            State.YESNOdv = YESNOdv

        End Sub

        Private Sub PopulateGrid()
            Dim dv As DataView

            Try
                If (State.searchDV Is Nothing) Then
                    State.searchDV = GetDV()
                End If
                State.searchDV.Sort = SortDirection

                If (State.IsAfterSave) Then
                    State.IsAfterSave = False
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.AcctBusinessUnitID, Grid, State.PageIndex)
                ElseIf (State.IsEditMode) Then
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.AcctBusinessUnitID, Grid, State.PageIndex, State.IsEditMode)
                Else
                    SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, Grid, State.PageIndex)
                End If

                Grid.AutoGenerateColumns = False
                'Me.Grid.Columns(Me.BUSINESS_UNIT_COL).SortExpression = AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_BUSINESS_UNIT
                'Me.Grid.Columns(Me.ACCT_COMPANY_DESCRIPTION_COL).SortExpression = AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_ACCT_COMPANY_DESCRIPTION
                SortAndBindGrid()

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

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
            HiddenDeletePromptResponse.Value = ""
        End Sub

        Private Sub DoDelete()
            State.MyBO = New AcctBusinessUnit(State.AcctBusinessUnitID)

            Try
                State.MyBO.Delete()
                State.MyBO.Save()
            Catch ex As Exception
                State.MyBO.RejectChanges()
                Throw ex
            End Try

            State.PageIndex = Grid.PageIndex

            'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
            State.IsAfterSave = True
            State.searchDV = Nothing
            PopulateGrid()
            State.PageIndex = Grid.PageIndex
        End Sub

        Private Function GetDV() As DataView

            Dim dv As DataView

            State.searchDV = GetGridDataView()
            State.searchDV.Sort = Grid.DataMember()

            Return (State.searchDV)

        End Function

        Private Function GetGridDataView() As DataView

            With State
                Return (AcctBusinessUnit.LoadList(.BusinessUnitMask, GetGuidFromString(.AcctCompanyIDMask), .MyAcctCompany))
            End With

        End Function

        Private Sub SetStateProperties()
            State.BusinessUnitMask = SearchBusinessUnitTextBox.Text
            State.AcctCompanyIDMask = SearchAcctCompanyDropdownList.SelectedValue
        End Sub

        Private Sub AddNew()

            Dim dv As DataView

            State.MyBO = New AcctBusinessUnit
            State.AcctBusinessUnitID = State.MyBO.Id

            'Check if searchDv is nothing.
            If State.searchDV Is Nothing Then GetDV()

            State.searchDV = State.MyBO.GetNewDataViewRow(State.searchDV, State.AcctBusinessUnitID, State.MyBO)
            Grid.DataSource = State.searchDV

            SetPageAndSelectedIndexFromGuid(State.searchDV, State.AcctBusinessUnitID, Grid, State.PageIndex, State.IsEditMode)

            State.bnoRow = False
            Grid.DataBind()

            State.PageIndex = Grid.PageIndex

            SetGridControls(Grid, False)

            'Set focus on the BusinessUnit TextBox for the EditItemIndex row
            SetFocusOnEditableFieldInGrid(Grid, BUSINESS_UNIT_COL, BUSINESS_UNIT_TEXTBOX, Grid.EditIndex)

            AssignSelectedRecordFromBO()

            'Me.TranslateGridControls(Grid)
            SetButtonsState()
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        End Sub

        Private Sub SortAndBindGrid()
            State.PageIndex = Grid.PageIndex
            'Me.Grid.DataSource = Me.State.searchDV
            'HighLightSortColumn(Grid, Me.SortDirection)
            'Me.Grid.DataBind()
            If (State.searchDV.Count = 0) Then

                State.bnoRow = True
                CreateHeaderForEmptyGrid(Grid, SortDirection)
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

        Private Sub AssignBOFromSelectedRecord()

            Try
                With State.MyBO
                    .BusinessUnit = CType(Grid.Rows(Grid.EditIndex).Cells(BUSINESS_UNIT_COL).FindControl(BUSINESS_UNIT_TEXTBOX), TextBox).Text
                    .Code = CType(Grid.Rows(Grid.EditIndex).Cells(CODE_COL).FindControl(CODE_TEXTBOX), TextBox).Text
                    .AcctCompanyId = GetGuidFromString(CType(Grid.Rows(Grid.EditIndex).Cells(ACCT_COMPANY_ID_COL).FindControl(ACCT_COMPANY_DROPLIST), DropDownList).SelectedValue())
                    .SuppressVendors = LookupListNew.GetCodeFromId(State.YESNOdv, New Guid(CType(Grid.Rows(Grid.EditIndex).Cells(SUPPRESS_VEND_COL).FindControl(SUPPRESS_VEND_DROPLIST), DropDownList).SelectedItem.Value))
                    .PaymentMethodId = GetGuidFromString(CType(Grid.Rows(Grid.EditIndex).Cells(PAYMENT_METHOD_COL).FindControl(PAYMENT_METHOD_DROPLIST), DropDownList).SelectedItem.Value)

                End With
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub AssignSelectedRecordFromBO()

            Dim gridRowIdx As Integer = Grid.EditIndex
            Try
                With State.MyBO
                    If .BusinessUnit IsNot Nothing Then
                        CType(Grid.Rows(gridRowIdx).Cells(BUSINESS_UNIT_COL).FindControl(BUSINESS_UNIT_TEXTBOX), TextBox).Text = .BusinessUnit
                        CType(Grid.Rows(gridRowIdx).Cells(CODE_COL).FindControl(CODE_TEXTBOX), TextBox).Text = .Code
                    End If

                    Dim AcctingCompList As DropDownList = CType(Grid.Rows(Grid.EditIndex).Cells(ACCT_COMPANY_DESCRIPTION_COL).Controls(1), DropDownList)

                    PopulateAcctCompanyDropdown(AcctingCompList)
                    SetSelectedItem(AcctingCompList, .AcctCompanyId)

                    CType(Grid.Rows(gridRowIdx).Cells(ACCT_BUSINESS_UNIT_ID_COL).FindControl(ACCT_BUSINESS_UNIT_ID_LABEL), Label).Text = .Id.ToString
                End With
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
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

        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
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
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Protected Sub ItemCommand(source As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Try
                Dim index As Integer

                If (e.CommandName = EDIT_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    State.IsEditMode = True
                    'e.Row.Cells(Me.ACCT_BUSINESS_UNIT_ID_COL).
                    State.AcctBusinessUnitID = New Guid(CType(Grid.Rows(index).Cells(ACCT_BUSINESS_UNIT_ID_COL).FindControl(ACCT_BUSINESS_UNIT_ID_LABEL), Label).Text)
                    'Me.State.AcctBusinessUnitID = New Guid(Me.Grid.Rows(index).Cells(Me.ACCT_BUSINESS_UNIT_ID_COL).Text)
                    State.MyBO = New AcctBusinessUnit(State.AcctBusinessUnitID)

                    PopulateGrid()

                    State.PageIndex = Grid.PageIndex

                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(Grid, False)

                    'Set focus on the Description TextBox for the EditItemIndex row
                    SetFocusOnEditableFieldInGrid(Grid, BUSINESS_UNIT_COL, BUSINESS_UNIT_TEXTBOX, index)

                    AssignSelectedRecordFromBO()

                    SetButtonsState()

                ElseIf (e.CommandName = DELETE_COMMAND) Then
                    index = CInt(e.CommandArgument)

                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    Grid.SelectedIndex = NO_ROW_SELECTED_INDEX

                    State.AcctBusinessUnitID = New Guid(CType(Grid.Rows(index).Cells(ACCT_BUSINESS_UNIT_ID_COL).FindControl(ACCT_BUSINESS_UNIT_ID_LABEL), Label).Text)
                    DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenDeletePromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
                End If

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        'The Binding Logic is here
        Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

            If dvRow IsNot Nothing And Not State.bnoRow Then

                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    CType(e.Row.Cells(ACCT_BUSINESS_UNIT_ID_COL).FindControl(ACCT_BUSINESS_UNIT_ID_LABEL), Label).Text = GetGuidStringFromByteArray(CType(dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_ACCT_BUSINESS_UNIT_ID), Byte()))
                    'e.Row.Cells(Me.ACCT_BUSINESS_UNIT_ID_COL).Text = GetGuidStringFromByteArray(CType(dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_ACCT_BUSINESS_UNIT_ID), Byte()))
                    e.Row.Cells(ACCT_COMPANY_ID_COL).Text = GetGuidStringFromByteArray(CType(dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_ACCT_COMPANY_ID), Byte()))
                    'e.Row.Cells(Me.BUSINESS_UNIT_COL).Text = dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_BUSINESS_UNIT).ToString
                    'e.Row.Cells(Me.ACCT_COMPANY_DESCRIPTION_COL).Controls()

                    If (State.IsEditMode = True _
                            AndAlso State.AcctBusinessUnitID.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_ACCT_BUSINESS_UNIT_ID), Byte())))) Then
                        CType(e.Row.Cells(BUSINESS_UNIT_COL).FindControl(BUSINESS_UNIT_TEXTBOX), TextBox).Text = dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_BUSINESS_UNIT).ToString
                        CType(e.Row.Cells(CODE_COL).FindControl(CODE_TEXTBOX), TextBox).Text = dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_CODE).ToString
                        CType(e.Row.Cells(ACCT_COMPANY_DESCRIPTION_COL).FindControl(ACCT_COMPANY_DROPLIST), DropDownList).Text = dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_ACCT_COMPANY_DESCRIPTION).ToString

                        '  BindListControlToDataView(CType(e.Row.Cells(Me.SUPPRESS_VEND_COL).FindControl(Me.SUPPRESS_VEND_DROPLIST), DropDownList), Me.State.YESNOdv, , , False)
                        Dim yesNoLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
                        CType(e.Row.Cells(SUPPRESS_VEND_COL).FindControl(SUPPRESS_VEND_DROPLIST), DropDownList).Populate(yesNoLkl, New PopulateOptions())

                        If dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_SUPPRESS_VENDORS) IsNot DBNull.Value Then
                            SetSelectedItem(CType(e.Row.Cells(SUPPRESS_VEND_COL).FindControl(SUPPRESS_VEND_DROPLIST), DropDownList), LookupListNew.GetIdFromCode(State.YESNOdv, dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_SUPPRESS_VENDORS).ToString))
                        End If

                        '  BindListControlToDataView(CType(e.Row.Cells(Me.PAYMENT_METHOD_COL).FindControl(Me.PAYMENT_METHOD_DROPLIST), DropDownList), LookupListNew.GetPaymentMethodLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , False) 'PMTHD

                        Dim paymentMethodLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("PMTHD", ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
                        CType(e.Row.Cells(PAYMENT_METHOD_COL).FindControl(PAYMENT_METHOD_DROPLIST), DropDownList).Populate(paymentMethodLkl, New PopulateOptions())
                        If dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_PAYMENT_METHOD) IsNot DBNull.Value AndAlso Not GuidControl.ByteArrayToGuid(dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_PAYMENT_METHOD)) = Guid.Empty Then
                            SetSelectedItem(CType(e.Row.Cells(PAYMENT_METHOD_COL).FindControl(PAYMENT_METHOD_DROPLIST), DropDownList), GetGuidStringFromByteArray(CType(dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_PAYMENT_METHOD), Byte())))
                        End If


                    Else
                        CType(e.Row.Cells(BUSINESS_UNIT_COL).FindControl(BUSINESS_UNIT_LABEL), Label).Text = dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_BUSINESS_UNIT).ToString
                        CType(e.Row.Cells(CODE_COL).FindControl(CODE_LABEL), Label).Text = dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_CODE).ToString
                        CType(e.Row.Cells(ACCT_COMPANY_DESCRIPTION_COL).FindControl(ACCT_COMPANY_DESCRIPTION_LABEL), Label).Text = dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_ACCT_COMPANY_DESCRIPTION).ToString

                        Dim suppressVendLabel As String = dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_SUPPRESS_VENDORS).ToString
                        Dim dRow() As DataRow = State.YESNOdv.Table.Select("code='" & suppressVendLabel & "' and language_id='" & GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.LanguageId) & "'")
                        If (dRow IsNot Nothing AndAlso dRow.Length > 0) Then
                            CType(e.Row.Cells(SUPPRESS_VEND_COL).FindControl(SUPPRESS_VEND_LABEL), Label).Text = CType(dRow(0).Item("Description"), String)
                        End If

                        If dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_PAYMENT_METHOD) IsNot DBNull.Value Then
                            CType(e.Row.Cells(PAYMENT_METHOD_COL).FindControl(PAYMENT_METHOD_LABEL), Label).Text = LookupListNew.GetDescriptionFromId(LookupListNew.GetPaymentMethodLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), New Guid(CType(dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_PAYMENT_METHOD), Byte())))
                        End If



                    End If
                    'e.Row.Cells(Me.ACCT_COMPANY_DESCRIPTION_COL).Text = dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_ACCT_COMPANY_DESCRIPTION).ToString
                End If
            End If
        End Sub
        Protected Sub ItemBound(source As Object, e As GridViewRowEventArgs) Handles Grid.RowDataBound

            Try
                If Not State.bnoRow Then
                    BaseItemBound(source, e)
                End If

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub


        Protected Sub BindBoPropertiesToGridHeaders()
            BindBOPropertyToGridHeader(State.MyBO, "BusinessUnit", Grid.Columns(BUSINESS_UNIT_COL))
            BindBOPropertyToGridHeader(State.MyBO, "Code", Grid.Columns(CODE_COL))
            BindBOPropertyToGridHeader(State.MyBO, "AcctCompanyId", Grid.Columns(ACCT_COMPANY_DESCRIPTION_COL))
            BindBOPropertyToGridHeader(State.MyBO, "SuppressVendors", Grid.Columns(SUPPRESS_VEND_COL))
            BindBOPropertyToGridHeader(State.MyBO, "PaymentMethodId", Grid.Columns(PAYMENT_METHOD_COL))
            ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(grid As GridView, cellPosition As Integer, controlName As String, itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim ctl As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(ctl)
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

                State.PageIndex = 0
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub
#End Region

    End Class

End Namespace
