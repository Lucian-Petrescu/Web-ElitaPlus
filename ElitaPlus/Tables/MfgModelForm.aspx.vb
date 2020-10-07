Option Strict On
Option Explicit On
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Tables

    Partial Class MfgModelForm
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
                IsEditing = (Grid.EditItemIndex > NO_ROW_SELECTED_INDEX)
            End Get
        End Property


        Public ReadOnly Property TheDealerControl() As MultipleColumnDDLabelControl
            Get
                If multipleDropControl Is Nothing Then
                    multipleDropControl = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return multipleDropControl
            End Get
        End Property

#End Region

#Region "Page State"
        Class MyState
            Public PageIndex As Integer = 0
            '  Public myBO As MfgModel = New MfgModel
            Public myBO As MfgModel
            Public DescriptionMask As String
            Public DealerSearchId As Guid
            Public ManufacturerSearchId As Guid
            Public CompanyId As Guid
            Public CompanyGroupId As Guid
            Public Id As Guid
            Public IsGridVisible As Boolean
            Public IsAfterSave As Boolean
            Public IsEditMode As Boolean
            Public searchDV As DataView = Nothing
            Public SortExpression As String = MfgModel.MfgModelSearchDV.COL_DEALER_NAME
            Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
            Public AddingNewRow As Boolean
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

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents ErrController As ErrorController

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

        Private Const MODIFY_BUTTON_IDX As Integer = 0
        Private Const DELETE_BUTTON_IDX As Integer = 1
        Private Const ID_COL_IDX As Integer = 2
        Private Const DEALER_COL_IDX As Integer = 3
        Private Const MANUFACTURER_COL_IDX As Integer = 4
        Private Const DESCRIPTION_COL_IDX As Integer = 5

        Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"
        Private Const MSG_CAN_NOT_EDIT_EDIT_MODEL_NOT_ENABLED As String = "MSG_CAN_NOT_EDIT_EDIT_MODEL_NOT_ENABLED"

        Private Const EDIT_COMMAND As String = "EditRecord"
        Private Const DELETE_COMMAND As String = "DeleteRecord"
        Private Const SORT_COMMAND As String = "Sort"

        Private Const MODIFY_IN_GRID_CONTROL_NAME As String = "EditButton_WRITE"
        Private Const DELETE_IN_GRID_CONTROL_NAME As String = "DeleteButton_WRITE"
        Private Const DESCRIPTION_IN_GRID_CONTROL_NAME As String = "TextBoxGridDescription"
        Private Const DEALER_LIST_IN_GRID_CONTROL_NAME As String = "cboDealerInGrid"
        Private Const MANUFACTURER_LIST_IN_GRID_CONTROL_NAME As String = "cboManufacturerInGrid"

        Private Const MANUFACTURER_DESCRIPTION_COL_NAME As String = "CODE"

        Private Const DEALER_CELL_STYLE_WIDTH As String = "199"
        Private Const MANUFACTURER_CELL_STYLE_WIDTH As String = "199"
        Private Const DESCRIPTION_CELL_STYLE_WIDTH As String = "175"

        'Private Const EDIT As String = "Edit"
        'Private Const DELETE As String = "Delete"

        Private Const NO_ROW_SELECTED_INDEX As Integer = -1

        Private Const LABEL_DEALER As String = "DEALER"

#End Region

#Region "Button Click Handlers"

        Private Sub SearchButton_Click(sender As System.Object, e As System.EventArgs) Handles SearchButton.Click
            Try
                State.PageIndex = 0
                State.Id = Guid.Empty
                State.IsGridVisible = True
                State.searchDV = Nothing
                PopulateGrid()
                State.PageIndex = Grid.CurrentPageIndex
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try

        End Sub

        Private Sub ClearButton_Click(sender As System.Object, e As System.EventArgs) Handles ClearButton.Click

            ClearSearchCriteria()

        End Sub

        Private Sub ClearSearchCriteria()

            Try
                SearchDescriptionTextBox.Text = String.Empty
                'boDealer.SelectedIndex = 0
                TheDealerControl.SelectedIndex = 0
                cboManufacturer.SelectedIndex = 0

                'Update Page State
                With State
                    .DescriptionMask = SearchDescriptionTextBox.Text
                    .DealerSearchId = Guid.Empty
                    .ManufacturerSearchId = Guid.Empty
                End With
            Catch ex As Exception
                HandleErrors(ex, ErrController)
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
                HandleErrors(ex, ErrController)
            End Try

        End Sub

        Private Sub SaveButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles SaveButton_WRITE.Click

            Try
                PopulateBOFromForm()
                If (State.myBO.IsDirty) Then
                    State.myBO.Save()
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
                HandleErrors(ex, ErrController)
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
                HandleErrors(ex, ErrController)
            End Try

        End Sub

#End Region

#Region "Private Methods"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

            'Put user code to initialize the page here
            Try
                ErrController.Clear_Hide()
                SetStateProperties()
                If Not Page.IsPostBack Then
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    SetDefaultButton(SearchDescriptionTextBox, SearchButton)
                    SetGridItemStyleColor(Grid)
                    If State.MyBO Is Nothing Then
                        State.myBO = New MfgModel
                    End If
                    PopulateDropdowns()
                    State.PageIndex = 0
                    SetButtonsState()
                End If
                BindBoPropertiesToGridHeaders()
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
            ShowMissingTranslations(ErrController)
        End Sub

        Private Sub PopulateGrid()

            Try
                'Refresh the DataView and Call SetPageAndSelectedIndexFromGuid() to go to the Page 
                'where the most recently saved Record exists in the DataView
                If (State.searchDV Is Nothing) Then
                    State.searchDV = GetGridDataView()
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
                Grid.Columns(DEALER_COL_IDX).SortExpression = MfgModel.MfgModelSearchDV.COL_DEALER_NAME
                Grid.Columns(MANUFACTURER_COL_IDX).SortExpression = MfgModel.MfgModelSearchDV.COL_MANUFACTURER_NAME
                Grid.Columns(DESCRIPTION_COL_IDX).SortExpression = MfgModel.MfgModelSearchDV.COL_DESCRIPTION

                SortAndBindGrid()

            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try

        End Sub

        Private Function GetGridDataView() As DataView

            With State
                Return (MfgModel.getList(.DescriptionMask, .DealerSearchId, .ManufacturerSearchId, .CompanyGroupId))
            End With

        End Function

        Private Sub SetStateProperties()

            State.DescriptionMask = SearchDescriptionTextBox.Text
            If (TheDealerControl.SelectedGuid.ToString IsNot Nothing AndAlso TheDealerControl.SelectedDesc.ToString <> NOTHING_SELECTED_TEXT) Then
                State.DealerSearchId = TheDealerControl.SelectedGuid
            Else
                State.DealerSearchId = Guid.Empty
            End If
            If (cboManufacturer.SelectedItem IsNot Nothing AndAlso cboManufacturer.SelectedItem.Value <> NOTHING_SELECTED_TEXT) Then
                State.ManufacturerSearchId = GetGuidFromString(cboManufacturer.SelectedItem.Value)
            Else
                State.ManufacturerSearchId = Guid.Empty
            End If
            State.CompanyId = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID
            State.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

        End Sub

        'Used to populate the dropdowns within the Search panel
        Private Sub PopulateDropdowns()

            'Me.BindListControlToDataView(Me.cboDealer, LookupListNew.GetDealerLookupList(Me.State.CompanyId))
            'Me.SetSelectedItem(Me.cboDealer, Me.State.DealerSearchId)

            Dim oDataView As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            TheDealerControl.Caption = TranslationBase.TranslateLabelOrMessage(LABEL_DEALER)
            TheDealerControl.NothingSelected = True
            TheDealerControl.BindData(oDataView)
            TheDealerControl.AutoPostBackDD = False
            TheDealerControl.NothingSelected = True
            TheDealerControl.SelectedGuid = State.DealerSearchId
            ' Me.BindListControlToDataView(Me.cboManufacturer, LookupListNew.GetManufacturerLookupList(Me.State.CompanyGroupId), MANUFACTURER_DESCRIPTION_COL_NAME, , True)
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyGroupId = State.CompanyGroupId
            Dim manufacturerLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ManufacturerByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), ListContext)
            cboManufacturer.Populate(manufacturerLkl, New PopulateOptions() With
            {
              .AddBlankItem = True,
              .TextFunc = AddressOf .GetCode
            })
            SetSelectedItem(cboManufacturer, State.ManufacturerSearchId)

        End Sub

        Private Sub AddNew()

            State.searchDV = GetGridDataView()

            State.myBO = New MfgModel
            State.Id = State.myBO.Id

            State.searchDV = State.myBO.GetNewDataViewRow(State.searchDV, State.Id)

            Grid.DataSource = State.searchDV
            SetGridControls(Grid, False)
            SetPageAndSelectedIndexFromGuid(State.searchDV, State.Id, Grid, State.PageIndex, State.IsEditMode)

            Grid.AutoGenerateColumns = False
            Grid.Columns(DEALER_COL_IDX).SortExpression = MfgModel.MfgModelSearchDV.COL_DEALER_NAME
            Grid.Columns(MANUFACTURER_COL_IDX).SortExpression = MfgModel.MfgModelSearchDV.COL_MANUFACTURER_NAME
            Grid.Columns(DESCRIPTION_COL_IDX).SortExpression = MfgModel.MfgModelSearchDV.COL_DESCRIPTION

            SortAndBindGrid()

            'Set focus on the Dealer dropdown list for the EditItemIndex row
            SetFocusOnEditableFieldInGrid(Grid, DEALER_COL_IDX, Grid.EditItemIndex)

        End Sub

        Private Sub SortAndBindGrid()
            State.PageIndex = Grid.CurrentPageIndex
            Grid.DataSource = State.searchDV
            HighLightSortColumn(Grid, State.SortExpression)
            Grid.DataBind()

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

        Private Sub PopulateBOFromForm()

            Try
                With State.myBO
                    .Description = CType(Grid.Items(Grid.EditItemIndex).Cells(DESCRIPTION_COL_IDX).FindControl(DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox).Text
                    .DealerId = GetSelectedItem(CType(Grid.Items(Grid.EditItemIndex).Cells(DEALER_COL_IDX).FindControl(DEALER_LIST_IN_GRID_CONTROL_NAME), DropDownList))
                    .ManufacturerId = GetSelectedItem(CType(Grid.Items(Grid.EditItemIndex).Cells(MANUFACTURER_COL_IDX).FindControl(MANUFACTURER_LIST_IN_GRID_CONTROL_NAME), DropDownList))
                End With
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try

        End Sub

        Private Sub ReturnFromEditing()

            Grid.EditItemIndex = NO_ROW_SELECTED_INDEX

            If Grid.PageCount = 0 Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, Grid, False)
            Else
                ControlMgr.SetVisibleControl(Me, Grid, True)
            End If
            SetGridControls(Grid, True)
            State.IsEditMode = False
            PopulateGrid()
            State.PageIndex = Grid.CurrentPageIndex
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

#Region " Datagrid Related "

        'The Binding Logic is here
        Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
            Try
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

                If (itemType = ListItemType.Item OrElse
                    itemType = ListItemType.AlternatingItem OrElse
                    itemType = ListItemType.SelectedItem) Then

                    If (State.AddingNewRow Or State.IsEditMode) Then
                        CType(e.Item.Cells(DELETE_BUTTON_IDX).FindControl(DELETE_IN_GRID_CONTROL_NAME), ImageButton).Visible = False
                        CType(e.Item.Cells(MODIFY_BUTTON_IDX).FindControl(MODIFY_IN_GRID_CONTROL_NAME), ImageButton).Visible = False
                    End If

                    e.Item.Cells(ID_COL_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(MfgModel.MfgModelSearchDV.COL_MFG_MODEL_ID), Byte()))
                    e.Item.Cells(DESCRIPTION_COL_IDX).Text = dvRow(MfgModel.MfgModelSearchDV.COL_DESCRIPTION).ToString
                    e.Item.Cells(DEALER_COL_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(MfgModel.MfgModelSearchDV.COL_DEALER_ID), Byte()))
                    e.Item.Cells(DEALER_COL_IDX).Text = dvRow(MfgModel.MfgModelSearchDV.COL_DEALER_NAME).ToString
                    e.Item.Cells(MANUFACTURER_COL_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(MfgModel.MfgModelSearchDV.COL_MANUFACTURER_ID), Byte()))
                    e.Item.Cells(MANUFACTURER_COL_IDX).Text = dvRow(MfgModel.MfgModelSearchDV.COL_MANUFACTURER_NAME).ToString

                ElseIf (itemType = ListItemType.EditItem) Then
                    e.Item.Cells(ID_COL_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(MfgModel.MfgModelSearchDV.COL_MFG_MODEL_ID), Byte()))
                    CType(e.Item.Cells(DESCRIPTION_COL_IDX).FindControl(DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox).Text = dvRow(MfgModel.MfgModelSearchDV.COL_DESCRIPTION).ToString

                    ' Me.BindListControlToDataView(CType(e.Item.Cells(Me.DEALER_COL_IDX).FindControl(DEALER_LIST_IN_GRID_CONTROL_NAME), DropDownList), LookupListNew.GetDealerEditModelLookupList())
                    '<DEALER_EDIT_MODEL>
                    Dim oDealerList As ListItem() = GetDealerListByCompanyForUser()
                    Dim dealerTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                                       Return li.Code + "-" + li.Translation
                                                                                   End Function
                    CType(e.Item.Cells(DEALER_COL_IDX).FindControl(DEALER_LIST_IN_GRID_CONTROL_NAME), DropDownList).Populate(oDealerList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True,
                                                    .TextFunc = dealerTextFunc,
                                                    .SortFunc = dealerTextFunc
                                                   })
                    SetSelectedItem(CType(e.Item.Cells(DEALER_COL_IDX).FindControl(DEALER_LIST_IN_GRID_CONTROL_NAME), DropDownList), State.myBO.DealerId)

                    'Me.BindListControlToDataView(CType(e.Item.Cells(Me.MANUFACTURER_COL_IDX).FindControl(MANUFACTURER_LIST_IN_GRID_CONTROL_NAME), DropDownList), LookupListNew.GetManufacturerLookupList(Me.State.CompanyGroupId), MANUFACTURER_DESCRIPTION_COL_NAME, , True)
                    Dim listcontext As ListContext = New ListContext()
                    listcontext.CompanyGroupId = State.CompanyGroupId
                    Dim manufacturerLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ManufacturerByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                    CType(e.Item.Cells(MANUFACTURER_COL_IDX).FindControl(MANUFACTURER_LIST_IN_GRID_CONTROL_NAME), DropDownList).Populate(manufacturerLkl, New PopulateOptions() With
                    {
                   .AddBlankItem = True,
                   .TextFunc = AddressOf .GetCode
                   })
                    SetSelectedItem(CType(e.Item.Cells(MANUFACTURER_COL_IDX).FindControl(MANUFACTURER_LIST_IN_GRID_CONTROL_NAME), DropDownList), State.myBO.ManufacturerId)
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try

        End Sub
        Private Function GetDealerListByCompanyForUser() As DataElements.ListItem()
            'Dim Index As Integer
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
            Dim comLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.Company, Thread.CurrentPrincipal.GetLanguageCode())
            'Dim UserCompanies As ArrayList = comLkl.ToArray()

            Dim oDealerList As New Collections.Generic.List(Of DataElements.ListItem)

            '  For Index = 0 To UserCompanies.Count - 1
            For Each _company As ListItem In comLkl
                'UserCompanyList &= ",'" & GuidControl.GuidToHexString(UserCompanyies(Index))
                oListContext.CompanyId = _company.ListItemId
                Dim oDealerListForCompany As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompanyAndEditModel", context:=oListContext)
                If oDealerListForCompany.Count > 0 Then
                    If oDealerList IsNot Nothing Then
                        oDealerList.AddRange(oDealerListForCompany)
                    Else
                        oDealerList = CType(oDealerListForCompany.Clone(),
                                            Collections.Generic.List(Of DataElements.ListItem))
                    End If

                End If
            Next

            Return oDealerList.ToArray()

        End Function

        Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged

            Try
                If (Not (State.IsEditMode)) Then
                    State.PageIndex = e.NewPageIndex
                    Grid.CurrentPageIndex = State.PageIndex
                    PopulateGrid()
                    Grid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try

        End Sub

        Protected Sub ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)

            Try
                Dim index As Integer = e.Item.ItemIndex

                If (e.CommandName = EDIT_COMMAND) Then
                    'Do the Edit here

                    State.Id = New Guid(Grid.Items(e.Item.ItemIndex).Cells(ID_COL_IDX).Text)

                    State.myBO = New MfgModel(State.Id)

                    ' Check if Dealer is in the List of Dealer with the Screen Enabled
                    Dim dealerDropDown As New DropDownList
                    'Me.BindListControlToDataView(dealerDropDown, LookupListNew.GetDealerEditModelLookupList()) '<DEALER_EDIT_MODEL>
                    Dim oDealerList As ListItem() = GetDealerListByCompanyForUser()
                    Dim dealerTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                                       Return li.Code + "-" + li.Translation
                                                                                   End Function
                    dealerDropDown.Populate(oDealerList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True,
                                                    .TextFunc = dealerTextFunc,
                                                    .SortFunc = dealerTextFunc
                                                   })
                    If (dealerDropDown.Items.FindByValue(State.myBO.DealerId.ToString) Is Nothing) Then
                        AddInfoMsg(MSG_CAN_NOT_EDIT_EDIT_MODEL_NOT_ENABLED)
                        Return
                    End If
                    'Set the IsEditMode flag to TRUE
                    State.IsEditMode = True

                    PopulateGrid()

                    State.PageIndex = Grid.CurrentPageIndex

                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(Grid, False)

                    'Set focus on the Dealer dropdown list for the EditItemIndex row
                    SetFocusOnEditableFieldInGrid(Grid, DEALER_COL_IDX, index)

                    SetButtonsState()

                ElseIf (e.CommandName = DELETE_COMMAND) Then
                    'Do the delete here

                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    Grid.SelectedIndex = NO_ROW_SELECTED_INDEX

                    'Save the Id in the Session
                    State.Id = New Guid(Grid.Items(e.Item.ItemIndex).Cells(ID_COL_IDX).Text)
                    State.myBO = New MfgModel(State.Id)

                    Try
                        State.myBO.Delete()
                        'Call the Save() method in the Business Object here
                        State.myBO.Save()
                    Catch ex As Exception
                        State.myBO.RejectChanges()
                        Throw ex
                    End Try

                    State.PageIndex = Grid.CurrentPageIndex

                    'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
                    State.IsAfterSave = True

                    State.searchDV = Nothing
                    PopulateGrid()
                    State.PageIndex = Grid.CurrentPageIndex
                End If

            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try

        End Sub

        Protected Sub ItemBound(source As Object, e As DataGridItemEventArgs) Handles Grid.ItemDataBound
            BaseItemBound(source, e)
        End Sub

        Protected Sub ItemCreated(sender As Object, e As DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub

        Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles Grid.SortCommand

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
                HandleErrors(ex, ErrController)
            End Try

        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()
            BindBOPropertyToGridHeader(State.myBO, "DealerId", Grid.Columns(DEALER_COL_IDX))
            BindBOPropertyToGridHeader(State.myBO, "ManufacturerId", Grid.Columns(MANUFACTURER_COL_IDX))
            BindBOPropertyToGridHeader(State.myBO, "Description", Grid.Columns(DESCRIPTION_COL_IDX))
            ClearGridHeadersAndLabelsErrSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(grid As DataGrid, cellPosition As Integer, itemIndex As Integer)

            'Set focus on the specified control on the EditItemIndex row for the grid
            Dim ctrl As DropDownList = CType(grid.Items(itemIndex).Cells(cellPosition).FindControl(DEALER_LIST_IN_GRID_CONTROL_NAME), DropDownList)
            ctrl.Style("width") = DEALER_CELL_STYLE_WIDTH
            SetSelectedItem(ctrl, State.myBO.DealerId)
            SetFocus(ctrl)
            Dim ctrlManufacturer As DropDownList = CType(grid.Items(itemIndex).Cells(cellPosition + 1).FindControl(MANUFACTURER_LIST_IN_GRID_CONTROL_NAME), DropDownList)
            ctrlManufacturer.Style("width") = MANUFACTURER_CELL_STYLE_WIDTH
            Dim ctrlDescription As TextBox = CType(grid.Items(itemIndex).Cells(cellPosition + 2).FindControl(DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox)
            ctrlDescription.Style("width") = DESCRIPTION_CELL_STYLE_WIDTH

        End Sub


#End Region


    End Class

End Namespace
