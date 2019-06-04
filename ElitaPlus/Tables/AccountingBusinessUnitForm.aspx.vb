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
                IsEditing = (Me.Grid.EditIndex > NO_ROW_SELECTED_INDEX)
            End Get
        End Property

        Private WriteOnly Property EnableControls() As Boolean
            Set(ByVal Value As Boolean)
                ControlMgr.SetEnableControl(Me, Me.SearchButton, Value)
                ControlMgr.SetEnableControl(Me, Me.ClearButton, Value)
                ControlMgr.SetEnableControl(Me, Me.NewButton_WRITE, Value)
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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

        Private Sub SearchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchButton.Click
            Try
                Me.State.IsGridVisible = True
                Me.State.searchDV = Nothing
                PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub ClearButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearButton.Click

            Try
                ClearSearchCriteria()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub ClearSearchCriteria()

            Try
                SearchBusinessUnitTextBox.Text = String.Empty
                SearchAcctCompanyDropdownList.SelectedIndex = Me.BLANK_ITEM_SELECTED

                'Update Page State
                With Me.State
                    .BusinessUnitMask = SearchBusinessUnitTextBox.Text
                    .AcctCompanyIDMask = String.Empty
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub NewButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewButton_WRITE.Click

            Try
                Me.State.IsEditMode = True
                Me.State.IsGridVisible = True
                Me.State.AddingNewRow = True
                AddNew()
                SetButtonsState()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub SaveButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton_WRITE.Click

            Try
                AssignBOFromSelectedRecord()
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
                Me.HandleErrors(ex, Me.ErrControllerMaster)
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
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

#End Region


#Region "Private Methods"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            'Put user code to initialize the page here
            Try
                ErrControllerMaster.Clear_Hide()

                Me.SetFormTitle(PAGETITLE)
                Me.SetFormTab(PAGETAB)

                Me.SetStateProperties()
                If Not Page.IsPostBack Then
                    Me.SortDirection = AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_BUSINESS_UNIT
                    Me.SetDefaultButton(Me.SearchBusinessUnitTextBox, Me.SearchButton)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Me.SetGridItemStyleColor(Grid)
                    Me.State.PageIndex = 0
                    If Me.State.MyBO Is Nothing Then
                        Me.State.MyBO = New AcctBusinessUnit
                    End If
                    SetButtonsState()
                    Me.TranslateGridHeader(Me.Grid)
                    Me.TranslateGridControls(Me.Grid)
                    PopulateYesNo()
                    PopulateAcctCompanyDropdown(SearchAcctCompanyDropdownList)
                Else
                    CheckIfComingFromDeleteConfirm()
                End If
                BindBoPropertiesToGridHeaders()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
            Me.ShowMissingTranslations(ErrControllerMaster)
        End Sub

        Private Sub PopulateAcctCompanyDropdown(ByVal listCtl As System.Web.UI.WebControls.DropDownList)
            If ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies.Length > 0 Then
                listCtl.Items.Add(New System.Web.UI.WebControls.ListItem("", Guid.Empty.ToString))
                Me.State.MyAcctCompany.Add(Guid.Empty)
                For Each _acctCo As AcctCompany In ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies
                    If Not _acctCo.IsNew Then
                        listCtl.Items.Add(New System.Web.UI.WebControls.ListItem(_acctCo.Description, _acctCo.Id.ToString))
                        Me.State.MyAcctCompany.Add(_acctCo.Id)
                    Else
                        Me.ErrControllerMaster.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_ACCOUNTING_COMPANIES_NOT_FOUND_ERR)
                        EnableControls = False
                        Exit Sub
                    End If
                Next
            End If
            'Me.BindListControlToDataView(dlistAcctCompany, LookupListNew.GetAcctCompanyLookupList())
        End Sub

        Private Sub PopulateYesNo()
            Dim YESNOdv As DataView = LookupListNew.DropdownLookupList(YESNO, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
            Me.State.YESNOdv = YESNOdv

        End Sub

        Private Sub PopulateGrid()
            Dim dv As DataView

            Try
                If (Me.State.searchDV Is Nothing) Then
                    Me.State.searchDV = GetDV()
                End If
                Me.State.searchDV.Sort = Me.SortDirection

                If (Me.State.IsAfterSave) Then
                    Me.State.IsAfterSave = False
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.AcctBusinessUnitID, Me.Grid, Me.State.PageIndex)
                ElseIf (Me.State.IsEditMode) Then
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.AcctBusinessUnitID, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
                Else
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.Grid, Me.State.PageIndex)
                End If

                Me.Grid.AutoGenerateColumns = False
                'Me.Grid.Columns(Me.BUSINESS_UNIT_COL).SortExpression = AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_BUSINESS_UNIT
                'Me.Grid.Columns(Me.ACCT_COMPANY_DESCRIPTION_COL).SortExpression = AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_ACCT_COMPANY_DESCRIPTION
                Me.SortAndBindGrid()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

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
            Me.HiddenDeletePromptResponse.Value = ""
        End Sub

        Private Sub DoDelete()
            Me.State.MyBO = New AcctBusinessUnit(Me.State.AcctBusinessUnitID)

            Try
                Me.State.MyBO.Delete()
                Me.State.MyBO.Save()
            Catch ex As Exception
                Me.State.MyBO.RejectChanges()
                Throw ex
            End Try

            Me.State.PageIndex = Grid.PageIndex

            'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
            Me.State.IsAfterSave = True
            Me.State.searchDV = Nothing
            PopulateGrid()
            Me.State.PageIndex = Grid.PageIndex
        End Sub

        Private Function GetDV() As DataView

            Dim dv As DataView

            Me.State.searchDV = GetGridDataView()
            Me.State.searchDV.Sort = Grid.DataMember()

            Return (Me.State.searchDV)

        End Function

        Private Function GetGridDataView() As DataView

            With State
                Return (AcctBusinessUnit.LoadList(.BusinessUnitMask, Me.GetGuidFromString(.AcctCompanyIDMask), .MyAcctCompany))
            End With

        End Function

        Private Sub SetStateProperties()
            Me.State.BusinessUnitMask = SearchBusinessUnitTextBox.Text
            Me.State.AcctCompanyIDMask = SearchAcctCompanyDropdownList.SelectedValue
        End Sub

        Private Sub AddNew()

            Dim dv As DataView

            Me.State.MyBO = New AcctBusinessUnit
            Me.State.AcctBusinessUnitID = Me.State.MyBO.Id

            'Check if searchDv is nothing.
            If Me.State.searchDV Is Nothing Then GetDV()

            Me.State.searchDV = Me.State.MyBO.GetNewDataViewRow(Me.State.searchDV, Me.State.AcctBusinessUnitID, Me.State.MyBO)
            Grid.DataSource = Me.State.searchDV

            Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.AcctBusinessUnitID, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)

            Me.State.bnoRow = False
            Grid.DataBind()

            Me.State.PageIndex = Grid.PageIndex

            SetGridControls(Me.Grid, False)

            'Set focus on the BusinessUnit TextBox for the EditItemIndex row
            Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.BUSINESS_UNIT_COL, Me.BUSINESS_UNIT_TEXTBOX, Me.Grid.EditIndex)

            Me.AssignSelectedRecordFromBO()

            'Me.TranslateGridControls(Grid)
            Me.SetButtonsState()
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        End Sub

        Private Sub SortAndBindGrid()
            Me.State.PageIndex = Me.Grid.PageIndex
            'Me.Grid.DataSource = Me.State.searchDV
            'HighLightSortColumn(Grid, Me.SortDirection)
            'Me.Grid.DataBind()
            If (Me.State.searchDV.Count = 0) Then

                Me.State.bnoRow = True
                CreateHeaderForEmptyGrid(Grid, Me.SortDirection)
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

        Private Sub AssignBOFromSelectedRecord()

            Try
                With Me.State.MyBO
                    .BusinessUnit = CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.BUSINESS_UNIT_COL).FindControl(Me.BUSINESS_UNIT_TEXTBOX), TextBox).Text
                    .Code = CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.CODE_COL).FindControl(Me.CODE_TEXTBOX), TextBox).Text
                    .AcctCompanyId = Me.GetGuidFromString(CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.ACCT_COMPANY_ID_COL).FindControl(Me.ACCT_COMPANY_DROPLIST), DropDownList).SelectedValue())
                    .SuppressVendors = LookupListNew.GetCodeFromId(Me.State.YESNOdv, New Guid(CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.SUPPRESS_VEND_COL).FindControl(Me.SUPPRESS_VEND_DROPLIST), DropDownList).SelectedItem.Value))
                    .PaymentMethodId = Me.GetGuidFromString(CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.PAYMENT_METHOD_COL).FindControl(Me.PAYMENT_METHOD_DROPLIST), DropDownList).SelectedItem.Value)

                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub AssignSelectedRecordFromBO()

            Dim gridRowIdx As Integer = Me.Grid.EditIndex
            Try
                With Me.State.MyBO
                    If Not .BusinessUnit Is Nothing Then
                        CType(Me.Grid.Rows(gridRowIdx).Cells(Me.BUSINESS_UNIT_COL).FindControl(Me.BUSINESS_UNIT_TEXTBOX), TextBox).Text = .BusinessUnit
                        CType(Me.Grid.Rows(gridRowIdx).Cells(Me.CODE_COL).FindControl(Me.CODE_TEXTBOX), TextBox).Text = .Code
                    End If

                    Dim AcctingCompList As DropDownList = CType(Grid.Rows(Grid.EditIndex).Cells(Me.ACCT_COMPANY_DESCRIPTION_COL).Controls(1), DropDownList)

                    PopulateAcctCompanyDropdown(AcctingCompList)
                    Me.SetSelectedItem(AcctingCompList, .AcctCompanyId)

                    CType(Me.Grid.Rows(gridRowIdx).Cells(Me.ACCT_BUSINESS_UNIT_ID_COL).FindControl(Me.ACCT_BUSINESS_UNIT_ID_LABEL), Label).Text = .Id.ToString
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
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

        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
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
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Protected Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Try
                Dim index As Integer

                If (e.CommandName = Me.EDIT_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    Me.State.IsEditMode = True
                    'e.Row.Cells(Me.ACCT_BUSINESS_UNIT_ID_COL).
                    Me.State.AcctBusinessUnitID = New Guid(CType(Me.Grid.Rows(index).Cells(Me.ACCT_BUSINESS_UNIT_ID_COL).FindControl(Me.ACCT_BUSINESS_UNIT_ID_LABEL), Label).Text)
                    'Me.State.AcctBusinessUnitID = New Guid(Me.Grid.Rows(index).Cells(Me.ACCT_BUSINESS_UNIT_ID_COL).Text)
                    Me.State.MyBO = New AcctBusinessUnit(Me.State.AcctBusinessUnitID)

                    Me.PopulateGrid()

                    Me.State.PageIndex = Grid.PageIndex

                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(Me.Grid, False)

                    'Set focus on the Description TextBox for the EditItemIndex row
                    Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.BUSINESS_UNIT_COL, Me.BUSINESS_UNIT_TEXTBOX, index)

                    Me.AssignSelectedRecordFromBO()

                    Me.SetButtonsState()

                ElseIf (e.CommandName = Me.DELETE_COMMAND) Then
                    index = CInt(e.CommandArgument)

                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    Grid.SelectedIndex = Me.NO_ROW_SELECTED_INDEX

                    Me.State.AcctBusinessUnitID = New Guid(CType(Me.Grid.Rows(index).Cells(Me.ACCT_BUSINESS_UNIT_ID_COL).FindControl(Me.ACCT_BUSINESS_UNIT_ID_LABEL), Label).Text)
                    Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenDeletePromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        'The Binding Logic is here
        Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

            If Not dvRow Is Nothing And Not Me.State.bnoRow Then

                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    CType(e.Row.Cells(Me.ACCT_BUSINESS_UNIT_ID_COL).FindControl(Me.ACCT_BUSINESS_UNIT_ID_LABEL), Label).Text = GetGuidStringFromByteArray(CType(dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_ACCT_BUSINESS_UNIT_ID), Byte()))
                    'e.Row.Cells(Me.ACCT_BUSINESS_UNIT_ID_COL).Text = GetGuidStringFromByteArray(CType(dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_ACCT_BUSINESS_UNIT_ID), Byte()))
                    e.Row.Cells(Me.ACCT_COMPANY_ID_COL).Text = GetGuidStringFromByteArray(CType(dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_ACCT_COMPANY_ID), Byte()))
                    'e.Row.Cells(Me.BUSINESS_UNIT_COL).Text = dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_BUSINESS_UNIT).ToString
                    'e.Row.Cells(Me.ACCT_COMPANY_DESCRIPTION_COL).Controls()

                    If (Me.State.IsEditMode = True _
                            AndAlso Me.State.AcctBusinessUnitID.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_ACCT_BUSINESS_UNIT_ID), Byte())))) Then
                        CType(e.Row.Cells(Me.BUSINESS_UNIT_COL).FindControl(Me.BUSINESS_UNIT_TEXTBOX), TextBox).Text = dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_BUSINESS_UNIT).ToString
                        CType(e.Row.Cells(Me.CODE_COL).FindControl(Me.CODE_TEXTBOX), TextBox).Text = dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_CODE).ToString
                        CType(e.Row.Cells(Me.ACCT_COMPANY_DESCRIPTION_COL).FindControl(Me.ACCT_COMPANY_DROPLIST), DropDownList).Text = dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_ACCT_COMPANY_DESCRIPTION).ToString

                        '  BindListControlToDataView(CType(e.Row.Cells(Me.SUPPRESS_VEND_COL).FindControl(Me.SUPPRESS_VEND_DROPLIST), DropDownList), Me.State.YESNOdv, , , False)
                        Dim yesNoLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
                        CType(e.Row.Cells(Me.SUPPRESS_VEND_COL).FindControl(Me.SUPPRESS_VEND_DROPLIST), DropDownList).Populate(yesNoLkl, New PopulateOptions())

                        If Not dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_SUPPRESS_VENDORS) Is DBNull.Value Then
                            Me.SetSelectedItem(CType(e.Row.Cells(Me.SUPPRESS_VEND_COL).FindControl(Me.SUPPRESS_VEND_DROPLIST), DropDownList), LookupListNew.GetIdFromCode(Me.State.YESNOdv, dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_SUPPRESS_VENDORS).ToString))
                        End If

                        '  BindListControlToDataView(CType(e.Row.Cells(Me.PAYMENT_METHOD_COL).FindControl(Me.PAYMENT_METHOD_DROPLIST), DropDownList), LookupListNew.GetPaymentMethodLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , False) 'PMTHD

                        Dim paymentMethodLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("PMTHD", ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
                        CType(e.Row.Cells(Me.PAYMENT_METHOD_COL).FindControl(Me.PAYMENT_METHOD_DROPLIST), DropDownList).Populate(paymentMethodLkl, New PopulateOptions())
                        If Not dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_PAYMENT_METHOD) Is DBNull.Value AndAlso Not GuidControl.ByteArrayToGuid(dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_PAYMENT_METHOD)) = Guid.Empty Then
                            Me.SetSelectedItem(CType(e.Row.Cells(Me.PAYMENT_METHOD_COL).FindControl(Me.PAYMENT_METHOD_DROPLIST), DropDownList), GetGuidStringFromByteArray(CType(dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_PAYMENT_METHOD), Byte())))
                        End If


                    Else
                        CType(e.Row.Cells(Me.BUSINESS_UNIT_COL).FindControl(Me.BUSINESS_UNIT_LABEL), Label).Text = dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_BUSINESS_UNIT).ToString
                        CType(e.Row.Cells(Me.CODE_COL).FindControl(Me.CODE_LABEL), Label).Text = dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_CODE).ToString
                        CType(e.Row.Cells(Me.ACCT_COMPANY_DESCRIPTION_COL).FindControl(Me.ACCT_COMPANY_DESCRIPTION_LABEL), Label).Text = dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_ACCT_COMPANY_DESCRIPTION).ToString

                        Dim suppressVendLabel As String = dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_SUPPRESS_VENDORS).ToString
                        Dim dRow() As DataRow = Me.State.YESNOdv.Table.Select("code='" & suppressVendLabel & "' and language_id='" & GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.LanguageId) & "'")
                        If (Not dRow Is Nothing AndAlso dRow.Length > 0) Then
                            CType(e.Row.Cells(Me.SUPPRESS_VEND_COL).FindControl(Me.SUPPRESS_VEND_LABEL), Label).Text = CType(dRow(0).Item("Description"), String)
                        End If

                        If Not dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_PAYMENT_METHOD) Is DBNull.Value Then
                            CType(e.Row.Cells(Me.PAYMENT_METHOD_COL).FindControl(Me.PAYMENT_METHOD_LABEL), Label).Text = LookupListNew.GetDescriptionFromId(LookupListNew.GetPaymentMethodLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), New Guid(CType(dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_PAYMENT_METHOD), Byte())))
                        End If



                    End If
                    'e.Row.Cells(Me.ACCT_COMPANY_DESCRIPTION_COL).Text = dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_ACCT_COMPANY_DESCRIPTION).ToString
                End If
            End If
        End Sub
        Protected Sub ItemBound(ByVal source As Object, ByVal e As GridViewRowEventArgs) Handles Grid.RowDataBound

            Try
                If Not Me.State.bnoRow Then
                    BaseItemBound(source, e)
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub


        Protected Sub BindBoPropertiesToGridHeaders()
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "BusinessUnit", Me.Grid.Columns(Me.BUSINESS_UNIT_COL))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "Code", Me.Grid.Columns(Me.CODE_COL))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "AcctCompanyId", Me.Grid.Columns(Me.ACCT_COMPANY_DESCRIPTION_COL))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "SuppressVendors", Me.Grid.Columns(Me.SUPPRESS_VEND_COL))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "PaymentMethodId", Me.Grid.Columns(Me.PAYMENT_METHOD_COL))
            Me.ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As GridView, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim ctl As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(ctl)
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

                Me.State.PageIndex = 0
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub
#End Region

    End Class

End Namespace
