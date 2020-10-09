Option Strict On
Option Explicit On
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.Security

Partial Class RegionForm
    Inherits ElitaPlusSearchPage

#Region "Page State"
    Class MyState
        Public PageIndex As Integer = 0
        Public MyBo As BusinessObjectsNew.Region  '= New DealerGroup
        Public DescriptionMask As String
        Public CodeMask As String
        Public Id As Guid
        Public IsAfterSave As Boolean
        Public IsEditMode As Boolean
        Public IsGridVisible As Boolean
        Public searchDV As BusinessObjectsNew.Region.RegionSearchDV = Nothing
        Public SortExpression As String = BusinessObjectsNew.Region.RegionSearchDV.COL_EXTENDED_CODE
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public AddingNewRow As Boolean
        Public Canceling As Boolean
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

#Region "Constants"

    Private Const NoRowSelectedIndexConstant As Integer = -1
    Private Const RegionIdConstant As Integer = 2
    Private Const CountryIdConstant As Integer = 3
    Private Const DescriptionConstant As Integer = 4
    Private Const RegionCodeConstant As Integer = 5
    Private Const AccountingCodeConstant As Integer = 6
    Private Const InvoiceTaxConstant As Integer = 7
    Private Const SortValueConstant As Integer = 8

    Private Const CountryInGridControlName As String = "cboCountryInGrid"
    Private Const DescriptionInGridControlName As String = "TextBoxGridDescription"
    Private Const ShortDescInGridControlName As String = "TextBoxShortDesc"
    Private Const AcctCodeInGridControlName As String = "TextBoxAcctCode"
    Private Const InvoiceTaxInGridControlName As String = "TextBoxInvoiceTaxGL"
    Private Const ExtendedCodeInGridControlName As String = "TextBoxExtendedCode"
    Private Const RegionIdLabel As String = "LABELRegionId"
    Private Const LabelDescription As String = "DescriptionLabel"
    Private Const LabelShortDescription As String = "ShortDescriptionLabel"
    Private Const LabelAccountingCode As String = "AccountingCodeLabel"
    Private Const LabelCountry As String = "CountryLabel"
    Private Const LabelInvoiceTax As String = "InvoiceTaxGLLabel"
    Private Const LabelExtendedCode As String = "ExtendedCodeLabel"

    Private Const MsgRecordSavedOk As String = "MSG_RECORD_SAVED_OK"
    Private Const MsgRecordNotSaved As String = "MSG_RECORD_NOT_SAVED"


#End Region

#Region "Variables"

    Private _moRegion As BusinessObjectsNew.Region
    Private _moRegionId As String

#End Region

#Region "Properties"

    Private ReadOnly Property TheRegion() As BusinessObjectsNew.Region
        Get
            If IsNewRegion() = True Then
                ' For creating, inserting
                _moRegion = New BusinessObjectsNew.Region
                RegionId = _moRegion.Id.ToString
            Else
                ' For updating, deleting
                _moRegion = New BusinessObjectsNew.Region(State.Id)
            End If

            Return _moRegion
        End Get
    End Property

    Private Property RegionId() As String
        Get
            If Grid.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                _moRegionId = CType(Grid.Rows(Grid.SelectedIndex).Cells(RegionIdConstant).FindControl(RegionIdLabel), Label).Text
            End If
            Return _moRegionId
        End Get
        Set(value As String)
            If Grid.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                SetSelectedGridText(Grid, RegionIdConstant, value)
            End If
            _moRegionId = value
        End Set
    End Property

    Private Property IsNewRegion() As Boolean
        Get
            Return State.AddingNewRow
        End Get
        Set(value As Boolean)
            State.AddingNewRow = value
        End Set
    End Property

#End Region

#Region "Handlers"

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents moErrorController As ErrorController

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(sender As System.Object, e As EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Handlers-Init"

    Private Sub Page_Load(sender As System.Object, e As EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            moErrorController.Clear_Hide()
            If RegionId Is Nothing Then
                _moRegionId = Guid.Empty.ToString
            Else
                _moRegionId = RegionId
            End If

            If Not Page.IsPostBack Then
                SetDefaultButton(SearchDescriptionTextBox, SearchButton)
                SetDefaultButton(SearchCodeTextbox, SearchButton)
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                SetGridItemStyleColor(Grid)
                IsNewRegion = False
                SetButtonsState(False)
                PopulateCountry()

                TranslateGridHeader(Grid)
                TranslateGridControls(Grid)
            Else
                CheckIfComingFromDeleteConfirm()
            End If
            BindBoPropertiesToGridHeaders()
        Catch ex As Exception
            HandleErrors(ex, moErrorController)
        End Try
        ShowMissingTranslations(moErrorController)
    End Sub

#End Region

#Region "Handlers-Buttons"

    Private Sub SearchButton_Click(sender As System.Object, e As EventArgs) Handles SearchButton.Click
        Try
            Grid.PageIndex = NO_PAGE_INDEX
            State.IsGridVisible = True
            State.searchDV = Nothing
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, moErrorController)
        End Try

    End Sub

    Private Sub ClearButton_Click(sender As System.Object, e As EventArgs) Handles ClearButton.Click
        ClearSearchCriteria()

    End Sub

    Private Sub NewButton_WRITE_Click(sender As System.Object, e As EventArgs) Handles NewButton_WRITE.Click
        Try
            State.IsEditMode = True
            State.IsGridVisible = True
            State.AddingNewRow = True
            AddNew()
            SetButtonsState()
        Catch ex As Exception
            HandleErrors(ex, moErrorController)
        End Try

    End Sub

    Private Sub SaveButton_WRITE_Click(sender As System.Object, e As EventArgs) Handles SaveButton_WRITE.Click
        Try
            SaveChanges()

        Catch ex As Exception
            HandleErrors(ex, moErrorController)
        End Try

    End Sub

    Private Sub CancelButton_Click(sender As System.Object, e As EventArgs) Handles CancelButton.Click
        Try
            Grid.SelectedIndex = NO_ITEM_SELECTED_INDEX
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
    Private Sub ClearSearchCriteria()

        Try
            SearchDescriptionTextBox.Text = String.Empty
            SearchCodeTextbox.Text = String.Empty
            moCountryDrop.SelectedIndex = BLANK_ITEM_SELECTED
            PopulateGrid()

        Catch ex As Exception
            HandleErrors(ex, moErrorController)
        End Try

    End Sub

#End Region

#Region "Grid"

    Private Sub PopulateGrid()

        Try
            'Refresh the DataView and Call SetPageAndSelectedIndexFromGuid() to go to the Page 
            'where the most recently saved Record exists in the DataView

            If (State.searchDV Is Nothing) Then
                State.searchDV = GetGridDataView()
                '  Me.State.searchDV.Sort = Me.Grid.DataMember
            End If
            'Ticket # 748479 - Search grids in Tables tab should not show pop-up message when number of retrieved record is over 1,000
            'Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
            State.searchDV.Sort = State.SortExpression

            If (State.IsAfterSave) Then
                State.IsAfterSave = False
                SetPageAndSelectedIndexFromGuid(State.searchDV, State.Id, Grid, State.PageIndex)
            ElseIf (State.IsEditMode) Then
                SetPageAndSelectedIndexFromGuid(State.searchDV, State.Id, Grid, State.PageIndex, State.IsEditMode)
            Else
                'In a Delete scenario...
                SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, Grid, State.PageIndex, State.IsEditMode)
            End If
            Grid.AutoGenerateColumns = False
            'Me.Grid.Columns(Me.DescriptionConstant).SortExpression = DealerGroup.DealerGroupSearchDV.COL_DESCRIPTION
            'Me.Grid.Columns(Me.RegionIdConstant).SortExpression = DealerGroup.DealerGroupSearchDV.COL_CODE

            SortAndBindGrid()

        Catch ex As Exception
            HandleErrors(ex, moErrorController)
        End Try

    End Sub

    Private Sub PopulateGridWithNoSort()

        Try
            'Refresh the DataView and Call SetPageAndSelectedIndexFromGuid() to go to the Page 
            'where the most recently saved Record exists in the DataView

            If (State.searchDV Is Nothing) Then
                State.searchDV = GetGridDataView()
                '  Me.State.searchDV.Sort = Me.Grid.DataMember
            End If
            'Ticket # 748479 - Search grids in Tables tab should not show pop-up message when number of retrieved record is over 1,000
            'Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
            ' Me.State.searchDV.Sort = Me.State.SortExpression

            If (State.IsAfterSave) Then
                State.IsAfterSave = False
                SetPageAndSelectedIndexFromGuid(State.searchDV, State.Id, Grid, State.PageIndex)
            ElseIf (State.IsEditMode) Then
                SetPageAndSelectedIndexFromGuid(State.searchDV, State.Id, Grid, State.PageIndex, State.IsEditMode)
            Else
                'In a Delete scenario...
                SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, Grid, State.PageIndex, State.IsEditMode)
            End If
            Grid.AutoGenerateColumns = False
            'Me.Grid.Columns(Me.DescriptionConstant).SortExpression = DealerGroup.DealerGroupSearchDV.COL_DESCRIPTION
            'Me.Grid.Columns(Me.RegionIdConstant).SortExpression = DealerGroup.DealerGroupSearchDV.COL_CODE

            'SortAndBindGrid()

            State.PageIndex = Grid.PageIndex
            Grid.DataSource = State.searchDV
            Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)

            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

            Session("recCount") = State.searchDV.Count
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        Catch ex As Exception
            HandleErrors(ex, moErrorController)
        End Try

    End Sub

    Private Sub SortAndBindGrid()
        State.PageIndex = Grid.PageIndex

        If CountryTax.isInvoiceTaxEnabled Then
            Grid.Columns(InvoiceTaxConstant).Visible = True
        Else
            Grid.Columns(InvoiceTaxConstant).Visible = False
        End If

        If (State.searchDV.Count = 0) Then
            CreateHeaderForEmptyGrid(Grid, State.SortExpression)
            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
            Grid.PagerSettings.Visible = True
            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
        Else
            Grid.DataSource = State.searchDV
            HighLightSortColumn(Grid, State.SortExpression)
            Grid.PagerSettings.Visible = True
            Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
        End If

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

    'The Binding Logic is here
    Private Sub Grid_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles Grid.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

            If dvRow IsNot Nothing AndAlso State.searchDV.Count > 0 Then
                If (itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem) AndAlso CType(e.Row.RowState, Int16) < 4 Then
                    CType(e.Row.Cells(RegionIdConstant).FindControl(RegionIdLabel), Label).Text = GetGuidStringFromByteArray(CType(dvRow(BusinessObjectsNew.Region.RegionSearchDV.COL_REGION_ID), Byte()))
                    CType(e.Row.Cells(DescriptionConstant).FindControl(LabelDescription), Label).Text = dvRow(BusinessObjectsNew.Region.RegionSearchDV.COL_DESCRIPTION).ToString
                    CType(e.Row.Cells(RegionCodeConstant).FindControl(LabelShortDescription), Label).Text = dvRow(BusinessObjectsNew.Region.RegionSearchDV.COL_CODE).ToString
                    CType(e.Row.Cells(AccountingCodeConstant).FindControl(LabelAccountingCode), Label).Text = dvRow(BusinessObjectsNew.Region.RegionSearchDV.COL_ACCOUNTING_CODE).ToString
                    CType(e.Row.Cells(CountryIdConstant).FindControl(LabelCountry), Label).Text = dvRow(BusinessObjectsNew.Region.RegionSearchDV.COL_COUNTRY_NAME).ToString
                    CType(e.Row.Cells(InvoiceTaxConstant).FindControl(LabelInvoiceTax), Label).Text = dvRow(BusinessObjectsNew.Region.RegionSearchDV.COL_INVOICE_TAX_GL).ToString
                    CType(e.Row.Cells(SortValueConstant).FindControl(LabelExtendedCode), Label).Text = dvRow(BusinessObjectsNew.Region.RegionSearchDV.COL_EXTENDED_CODE).ToString

                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, moErrorController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(source As Object, e As GridViewPageEventArgs) Handles Grid.PageIndexChanging

        Try
            If (Not (State.IsEditMode)) Then
                State.PageIndex = e.NewPageIndex
                Grid.PageIndex = State.PageIndex
                PopulateGrid()
                Grid.SelectedIndex = NO_ITEM_SELECTED_INDEX
            End If
        Catch ex As Exception
            HandleErrors(ex, moErrorController)
        End Try

    End Sub

    Protected Sub RowCommand(source As Object, e As GridViewCommandEventArgs)

        Try
            Dim index As Integer

            If (e.CommandName = EDIT_COMMAND_NAME) Then

                index = CInt(e.CommandArgument)

                'Do the Edit here

                'Set the IsEditMode flag to TRUE
                State.IsEditMode = True

                State.Id = New Guid(CType(Grid.Rows(index).Cells(RegionIdConstant).FindControl(RegionIdLabel), Label).Text)
                State.MyBo = New BusinessObjectsNew.Region(State.Id)

                PopulateGrid()

                State.PageIndex = Grid.PageIndex

                'Disable all Edit and Delete icon buttons on the Grid
                SetGridControls(Grid, False)

                'Set focus on the Description TextBox for the EditItemIndex row
                SetFocusOnEditableFieldInGrid(Grid, DescriptionConstant, DescriptionInGridControlName, index)

                PopulateFormFromBo()

                SetButtonsState()

            ElseIf (e.CommandName = DELETE_COMMAND_NAME) Then
                index = CInt(e.CommandArgument)

                'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                Grid.SelectedIndex = NoRowSelectedIndexConstant
                State.Id = New Guid(CType(Grid.Rows(index).Cells(RegionIdConstant).FindControl(RegionIdLabel), Label).Text)

                DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenDeletePromptResponse)
                State.ActionInProgress = DetailPageCommand.Delete
            End If

        Catch ex As Exception
            HandleErrors(ex, moErrorController)
        End Try

    End Sub
    Private Sub PopulateFormFromBo()

        Dim gridRowIdx As Integer = Grid.EditIndex
        Dim txtShortDesc, txtAcctCode, txtInvoiceTax, txtExtendedCode As TextBox

        Try
            With State.MyBo

                'populateAcctCode()

                txtShortDesc = CType(Grid.Rows(gridRowIdx).Cells(RegionCodeConstant).FindControl(ShortDescInGridControlName), TextBox)
                If (.ShortDesc IsNot Nothing) Then
                    txtShortDesc.Text = .ShortDesc
                End If

                If (.Description IsNot Nothing) Then
                    CType(Grid.Rows(gridRowIdx).Cells(DescriptionConstant).FindControl(DescriptionInGridControlName), TextBox).Text = .Description
                End If

                txtAcctCode = CType(Grid.Rows(gridRowIdx).Cells(AccountingCodeConstant).FindControl(AcctCodeInGridControlName), TextBox)
                If (.AccountingCode IsNot Nothing) Then
                    txtAcctCode.Text = .AccountingCode
                End If

                txtInvoiceTax = CType(Grid.Rows(gridRowIdx).Cells(InvoiceTaxConstant).FindControl(InvoiceTaxInGridControlName), TextBox)
                If (.AccountingCode IsNot Nothing) Then
                    'Def-26691: Added condition to check if the value for InvoiceTaxGLAcct is not null.
                    If (.InvoiceTaxGLAcct IsNot Nothing) Then
                        txtInvoiceTax.Text = .InvoiceTaxGLAcct
                    End If
                End If

                txtExtendedCode = CType(Grid.Rows(gridRowIdx).Cells(SortValueConstant).FindControl(ExtendedCodeInGridControlName), TextBox)
                If (.ExtendedCode IsNot Nothing) Then
                    txtExtendedCode.Text = .ExtendedCode
                End If

                txtShortDesc.Attributes.Add("onBlur", "populateAcctCode(this, getElementById('" + txtAcctCode.ClientID + "'));")

                CType(Grid.Rows(gridRowIdx).Cells(RegionIdConstant).FindControl(RegionIdLabel), Label).Text = .Id.ToString

                Dim oCboCountry As DropDownList = CType(Grid.Rows(gridRowIdx).Cells(CountryIdConstant).FindControl(CountryInGridControlName), DropDownList)
                '   Me.BindListControlToDataView(oCboCountry, LookupListNew.GetUserCountriesLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies))
                Dim countryLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.Country, Thread.CurrentPrincipal.GetLanguageCode())
                Dim list As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Countries
                Dim filteredList As ListItem() = (From x In countryLkl
                                                  Where list.Contains(x.ListItemId)
                                                  Select x).ToArray()

                oCboCountry.Populate(filteredList, New PopulateOptions() With
                {
               .AddBlankItem = True
                 })
                oCboCountry.SelectedValue = moCountryDrop.SelectedValue

            End With
        Catch ex As Exception
            HandleErrors(ex, moErrorController)
        End Try

    End Sub
    Protected Sub RowCreated(sender As Object, e As GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_Sorting(source As Object, e As GridViewSortEventArgs) Handles Grid.Sorting

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
            State.Id = Guid.Empty
            State.PageIndex = 0

            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, moErrorController)
        End Try

    End Sub

    Protected Sub BindBoPropertiesToGridHeaders()
        BindBOPropertyToGridHeader(State.MyBo, "Description", Grid.Columns(DescriptionConstant))
        BindBOPropertyToGridHeader(State.MyBo, "ShortDesc", Grid.Columns(RegionCodeConstant))
        BindBOPropertyToGridHeader(State.MyBo, "AccountingCode", Grid.Columns(AccountingCodeConstant))
        BindBOPropertyToGridHeader(State.MyBo, "CountryId", Grid.Columns(CountryIdConstant))
        BindBOPropertyToGridHeader(State.MyBo, "InvoiceTaxGLAcct", Grid.Columns(InvoiceTaxConstant))
        ClearGridViewHeadersAndLabelsErrSign()
    End Sub

    Private Sub SetFocusOnEditableFieldInGrid(grid As GridView, cellPosition As Integer, controlName As String, itemIndex As Integer)
        'Set focus on the Description TextBox for the EditItemIndex row
        Dim desc As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
        SetFocus(desc)
    End Sub
    Protected Sub CheckIfComingFromDeleteConfirm()
        Dim confResponse As String = HiddenDeletePromptResponse.Value
        If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
            If State.ActionInProgress = DetailPageCommand.Delete Then
                DeleteSelectedRegion()
            End If
            Select Case State.ActionInProgress
                Case DetailPageCommand.Delete
            End Select
        ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
            Select Case State.ActionInProgress
                Case DetailPageCommand.Delete
            End Select
        End If
        'Clean after consuming the action
        State.ActionInProgress = DetailPageCommand.Nothing_
        HiddenDeletePromptResponse.Value = ""
    End Sub
#End Region

#Region "Handlers_DropDowns"

    Private Sub Grid_PageSizeChanged(source As Object, e As EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, moErrorController)
        End Try
    End Sub

#End Region

#End Region

#Region "Buttons-Management"

    Private Sub SetButtonsState(bIsEdit As Boolean)

        If (bIsEdit) Then
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

    'Private Sub SetButtonsState()

    '    If (Me.State.IsEditMode) Then
    '        ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, True)
    '        ControlMgr.SetVisibleControl(Me, CancelButton, True)
    '        ControlMgr.SetVisibleControl(Me, NewButton_WRITE, False)
    '        ControlMgr.SetEnableControl(Me, SearchButton, False)
    '        ControlMgr.SetEnableControl(Me, ClearButton, False)
    '        Me.MenuEnabled = False
    '        If (Me.cboPageSize.Visible) Then
    '            ControlMgr.SetEnableControl(Me, cboPageSize, False)
    '        End If
    '    Else
    '        ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, False)
    '        ControlMgr.SetVisibleControl(Me, CancelButton, False)
    '        ControlMgr.SetVisibleControl(Me, NewButton_WRITE, True)
    '        ControlMgr.SetEnableControl(Me, SearchButton, True)
    '        ControlMgr.SetEnableControl(Me, ClearButton, True)
    '        Me.MenuEnabled = True
    '        If (Me.cboPageSize.Visible) Then
    '            ControlMgr.SetEnableControl(Me, cboPageSize, True)
    '        End If
    '    End If

    'End Sub
#End Region

#Region "Populate"

    Private Sub PopulateCountry()
        'Me.BindListControlToDataView(moCountryDrop, LookupListNew.GetUserCountriesLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies))
        Dim countryLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.Country, Thread.CurrentPrincipal.GetLanguageCode())
        Dim list As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Countries
        Dim filteredList As ListItem() = (From x In countryLkl
                                          Where list.Contains(x.ListItemId)
                                          Select x).ToArray()

        moCountryDrop.Populate(filteredList, New PopulateOptions() With
             {
            .AddBlankItem = True
             })
        If moCountryDrop.Items.Count < 3 Then
            ControlMgr.SetVisibleControl(Me, moCountryLabel, False)
            ControlMgr.SetVisibleControl(Me, moCountryDrop, False)
        End If
    End Sub

    Public Overrides Sub BaseSetButtonsState(bIsEdit As Boolean)
        SetButtonsState(bIsEdit)
    End Sub

#End Region

#Region "Business"

    Private Sub PopulateBoFromForm(oRegion As BusinessObjectsNew.Region)

        Try
            With oRegion
                .Description = CType(Grid.Rows(Grid.EditIndex).Cells(DescriptionConstant).FindControl(DescriptionInGridControlName), TextBox).Text
                .ShortDesc = CType(Grid.Rows(Grid.EditIndex).Cells(RegionCodeConstant).FindControl(ShortDescInGridControlName), TextBox).Text
                .AccountingCode = CType(Grid.Rows(Grid.EditIndex).Cells(AccountingCodeConstant).FindControl(AcctCodeInGridControlName), TextBox).Text
                .ExtendedCode = CType(Grid.Rows(Grid.EditIndex).Cells(SortValueConstant).FindControl(ExtendedCodeInGridControlName), TextBox).Text

                'Def-26691: Added condition to check if the value for invoice tax is not empty.
                If Not (CType(Grid.Rows(Grid.EditIndex).Cells(InvoiceTaxConstant).FindControl(InvoiceTaxInGridControlName), TextBox).Text = String.Empty) AndAlso _
                    Not CType(Grid.Rows(Grid.EditIndex).Cells(InvoiceTaxConstant).FindControl(InvoiceTaxInGridControlName), TextBox).Text.Equals("") Then
                    .InvoiceTaxGLAcct = CType(Grid.Rows(Grid.EditIndex).Cells(InvoiceTaxConstant).FindControl(InvoiceTaxInGridControlName), TextBox).Text
                End If

                Dim countryIdList As DropDownList = CType(Grid.Rows(Grid.EditIndex).Cells(CountryIdConstant).FindControl(CountryInGridControlName), DropDownList)
                PopulateBOProperty(State.MyBo, "CountryId", countryIdList)

            End With
        Catch ex As Exception
            HandleErrors(ex, moErrorController)
        End Try

    End Sub
    Private Sub SaveChanges()
        Try
            PopulateBoFromForm(State.MyBo)
            If (State.MyBo.IsDirty) Then
                State.MyBo.Save()
                State.IsAfterSave = True
                State.AddingNewRow = False
                AddInfoMsg(MsgRecordSavedOk)
                State.searchDV = Nothing
                ReturnFromEditing()
            Else
                AddInfoMsg(MsgRecordNotSaved)
                ReturnFromEditing()
            End If
        Catch ex As Exception
            HandleErrors(ex, moErrorController)
        End Try
    End Sub
    Private Function GetDv() As DataView

        Dim dv As DataView

        dv = GetGridDataView()
        dv.Sort = Grid.DataMember()
        Grid.DataSource = dv

        Return (dv)

    End Function
    Private Function GetGridDataView() As BusinessObjectsNew.Region.RegionSearchDV
        Dim sDescription As String = "*"
        Dim sCode As String = "*"

        If Not SearchDescriptionTextBox.Text.Trim.Equals("") Then
            sDescription = SearchDescriptionTextBox.Text
        End If

        If Not SearchCodeTextbox.Text.Trim.Equals("") Then
            sCode = SearchCodeTextbox.Text
        End If

        If Not moCountryDrop.Visible Then moCountryDrop.SelectedIndex = 1
        Return New BusinessObjectsNew.Region.RegionSearchDV(BusinessObjectsNew.Region.LoadList(sDescription, sCode, GetGuidFromString(moCountryDrop.SelectedValue.ToString)).Table)

    End Function
    Private Function ApplyChanges() As Boolean
        Dim bIsOk As Boolean = True
        Dim bIsDirty As Boolean
        If Grid.EditIndex = NO_ITEM_SELECTED_INDEX Then Return False ' Product Code Conversion is not in edit mode
        Dim oRegion As BusinessObjectsNew.Region
        Try
            oRegion = TheRegion
            BindBoPropertiesToGridHeaders(oRegion)
            With oRegion
                PopulateBoFromForm(oRegion)
                bIsDirty = .IsDirty
                .Save()
                SetButtonsState(False)
                SetGridControls(Grid, True)
            End With
        Catch ex As Exception
            HandleErrors(ex, moErrorController)
            bIsOk = False
        End Try
        If (bIsOk = True) Then
            If bIsDirty = True Then
                AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
            Else
                AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
            End If
        End If

        Return bIsOk
    End Function
    Private Function DeleteSelectedRegion() As Boolean
        Dim bIsOk As Boolean = True
        Try
            With TheRegion()
                .Delete()
                .Save()
            End With

            'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
            State.IsAfterSave = True
            State.searchDV = Nothing
            PopulateGrid()
            State.PageIndex = Grid.PageIndex

        Catch ex As Exception
            HandleErrors(ex, moErrorController)
            bIsOk = False
        End Try
        Return bIsOk
    End Function
    Protected Sub BindBoPropertiesToGridHeaders(oRegion As BusinessObjectsNew.Region)
        BindBOPropertyToGridHeader(oRegion, "CountryId", Grid.Columns(CountryIdConstant))
        BindBOPropertyToGridHeader(oRegion, "Description", Grid.Columns(DescriptionConstant))
        BindBOPropertyToGridHeader(oRegion, "ShortDesc", Grid.Columns(RegionCodeConstant))
        BindBOPropertyToGridHeader(oRegion, "AccountingCode", Grid.Columns(AccountingCodeConstant))
        BindBOPropertyToGridHeader(oRegion, "InvoiceTaxGLAcct", Grid.Columns(InvoiceTaxConstant))
        ClearGridHeadersAndLabelsErrSign()
    End Sub
    Private Sub AddNew()

        State.searchDV = GetGridDataView()

        State.MyBo = New BusinessObjectsNew.Region
        State.Id = State.MyBo.Id

        State.searchDV = CType(State.MyBo.GetNewDataViewRow(State.searchDV, State.Id), BusinessObjectsNew.Region.RegionSearchDV)
        Grid.DataSource = State.searchDV

        SetPageAndSelectedIndexFromGuid(State.searchDV, State.Id, Grid, State.PageIndex, State.IsEditMode)

        SortAndBindGrid()

        State.PageIndex = Grid.PageIndex

        SetGridControls(Grid, False)

        'Set focus on the Description TextBox for the EditItemIndex row
        SetFocusOnEditableFieldInGrid(Grid, DescriptionConstant, DescriptionInGridControlName, Grid.EditIndex)
        PopulateFormFromBo()

        'Me.TranslateGridControls(Grid)
        SetButtonsState()
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)

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
    Private Sub ReturnFromEditing()

        Grid.EditIndex = NoRowSelectedIndexConstant

        If Grid.PageCount = 0 Then
            'if returning to the "1st time in" screen
            ControlMgr.SetVisibleControl(Me, Grid, False)
        Else
            ControlMgr.SetVisibleControl(Me, Grid, True)
        End If

        SetGridControls(Grid, True)
        State.IsEditMode = False
        PopulateGrid()
        State.PageIndex = Grid.PageIndex
        SetButtonsState()

    End Sub


#End Region

#Region "Overrides"

    Public Overrides Sub AddNewBoRow(oDataView As DataView)

        Dim oId As Guid = Guid.NewGuid
        BaseAddNewGridRow(Grid, oDataView, oId)
        State.Id = oId

    End Sub

    Protected Sub Grid_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Grid.SelectedIndexChanged

    End Sub

#End Region


End Class
