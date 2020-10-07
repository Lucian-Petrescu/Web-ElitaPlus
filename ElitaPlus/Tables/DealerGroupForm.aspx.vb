Option Strict On
Option Explicit On
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading

Namespace Tables

    Partial Class DealerGroupForm
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

#End Region

#Region "Page State"
        Class MyState
            Public PageIndex As Integer = 0
            Public MyBO As DealerGroup  '= New DealerGroup
            Public DescriptionMask As String
            Public CodeMask As String
            Public CompanyGroupId As Guid
            Public Id As Guid
            Public IsAfterSave As Boolean
            Public IsEditMode As Boolean
            Public IsGridVisible As Boolean
            Public searchDV As DataView = Nothing
            Public SortExpression As String = DealerGroup.DealerGroupSearchDV.COL_DESCRIPTION
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

#Region " Web Form Designer Generated Code "

        Protected WithEvents ErrController As ErrorController
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label
        Protected WithEvents Label2 As System.Web.UI.WebControls.Label

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Constants"

        Private Const ID_COL_IDX As Integer = 2
        Private Const DESCRIPTION_COL_IDX As Integer = 3
        Private Const CODE_COL_IDX As Integer = 4
        Private Const ACCTING_BY_GROUP_IDX As Integer = 5
        Private Const USE_CLIENT_DEALER_CODE_IDX As Integer = 6
        Private Const AUTO_REJ_ERR_TYPE_IDX As Integer = 7

        Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

        Private Const DESCRIPTION_CONTROL_NAME As String = "DescriptionTextBox"
        Private Const DESC_LABEL_CONTROL_NAME As String = "DescriptionLabel"
        Private Const CODE_CONTROL_NAME As String = "CodeTextBox"
        Private Const CODE_LABEL_CONTROL_NAME As String = "CodeLabel"
        Private Const ID_CONTROL_NAME As String = "IdLabel"
        Private Const ACCTBYGROUP_CONTROL_NAME As String = "AcctingByGroupDropdown"
        Private Const USECLIENTDEALERCODE_CONTROL_NAME As String = "UseClientDealerCodeDropdown"
        Private Const AUTOREJERRTYPE_CONTROL_NAME As String = "AutoRejErrTypeDropdown"
        Private Const ACCTBYGROUP_LABEL_CONTROL_NAME As String = "AcctingByGroupLabel"

        Private Const EDIT_COMMAND As String = "EditRecord"
        Private Const DELETE_COMMAND As String = "DeleteRecord"
        Private Const SORT_COMMAND As String = "Sort"

        Private Const NO_ROW_SELECTED_INDEX As Integer = -1
        Public Const PAGETITLE As String = "Dealer Group"
        Public Const PAGETAB As String = "TABLES"

#End Region

#Region "Button Click Handlers"

        Private Sub SearchButton_Click(sender As System.Object, e As System.EventArgs) Handles SearchButton.Click
            Try
                State.PageIndex = 0
                State.Id = Guid.Empty
                State.IsGridVisible = True
                State.searchDV = Nothing
                PopulateGrid()
                State.PageIndex = Grid.PageIndex
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub ClearButton_Click(sender As System.Object, e As System.EventArgs) Handles ClearButton.Click

            Try
                ClearSearchCriteria()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ClearSearchCriteria()

            Try
                SearchDescriptionTextBox.Text = String.Empty
                SearchCodeTextBox.Text = String.Empty

                'Update Page State
                With State
                    .DescriptionMask = SearchDescriptionTextBox.Text
                    .CodeMask = SearchCodeTextBox.Text
                End With

                ReturnFromEditing()

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
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
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub SaveButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles SaveButton_WRITE.Click
            Try
                PopulateBOFromForm()
                If (State.MyBO.IsDirty) Then
                    State.MyBO.Save()
                    State.IsAfterSave = True
                    State.AddingNewRow = False
                    'Me.AddInfoMsg(Me.MSG_RECORD_SAVED_OK)
                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                    State.searchDV = Nothing
                    ReturnFromEditing()
                Else
                    'Me.AddInfoMsg(Me.MSG_RECORD_NOT_SAVED)
                    MasterPage.MessageController.AddWarning(Message.MSG_RECORD_NOT_SAVED)
                    ReturnFromEditing()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
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
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

#End Region

#Region "Private Methods"
        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            End If
        End Sub
        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

            'Put user code to initialize the page here
            Try
                SetFormTab(PAGETAB)
                SetFormTitle(PAGETITLE)

                MasterPage.MessageController.Clear_Hide()
                SetStateProperties()

                MasterPage.MessageController.Clear()
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
                UpdateBreadCrum()

                If Not Page.IsPostBack Then
                    SetDefaultButton(SearchCodeTextBox, SearchButton)
                    SetDefaultButton(SearchDescriptionTextBox, SearchButton)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    SetGridItemStyleColor(Grid)
                    State.PageIndex = 0
                    SetButtonsState()
                    If State.MyBO Is Nothing Then
                        State.MyBO = New DealerGroup
                    End If
                    TranslateGridHeader(Grid)
                    TranslateGridControls(Grid)
                Else
                    CheckIfComingFromDeleteConfirm()
                End If
                BindBoPropertiesToGridHeaders()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
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
            State.MyBO = New DealerGroup(State.Id)
            Try
                State.MyBO.Delete()
                'Call the Save() method in the DealerGroup Business Object here
                State.MyBO.Save()
                'Me.AddInfoMsg(Me.MSG_RECORD_DELETED_OK)
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
                'Me.Grid.Columns(Me.DESCRIPTION_COL_IDX).SortExpression = DealerGroup.DealerGroupSearchDV.COL_DESCRIPTION
                'Me.Grid.Columns(Me.CODE_COL_IDX).SortExpression = DealerGroup.DealerGroupSearchDV.COL_CODE

                SortAndBindGrid()

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
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
                'Me.Grid.Columns(Me.DESCRIPTION_COL_IDX).SortExpression = DealerGroup.DealerGroupSearchDV.COL_DESCRIPTION
                'Me.Grid.Columns(Me.CODE_COL_IDX).SortExpression = DealerGroup.DealerGroupSearchDV.COL_CODE

                'SortAndBindGrid()

                State.PageIndex = Grid.PageIndex
                Grid.DataSource = State.searchDV
                Grid.DataBind()

                ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)

                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

                Session("recCount") = State.searchDV.Count
                ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub


        Private Function GetGridDataView() As DataView

            With State
                Return (DealerGroup.LoadList(.DescriptionMask, .CodeMask))
            End With

        End Function

        Private Sub SetStateProperties()

            State.DescriptionMask = SearchDescriptionTextBox.Text
            State.CodeMask = SearchCodeTextBox.Text
            State.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

        End Sub

        Private Sub SortAndBindGrid()
            State.PageIndex = Grid.PageIndex
            If (State.searchDV.Count = 0) Then
                CreateHeaderForEmptyGrid(Grid1, State.SortExpression)
                ControlMgr.SetVisibleControl(Me, Grid1, State.IsGridVisible)
                ControlMgr.SetVisibleControl(Me, Grid, False)
                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
                Grid1.PagerSettings.Visible = True
                If Not Grid1.BottomPagerRow.Visible Then Grid1.BottomPagerRow.Visible = True
            Else
                Grid.DataSource = State.searchDV
                HighLightSortColumn(Grid, State.SortExpression)
                Grid.DataBind()
                Grid.PagerSettings.Visible = True
                ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
                ControlMgr.SetVisibleControl(Me, Grid1, False)
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


        Private Sub AddNew()
            State.searchDV = GetGridDataView()

            State.MyBO = New DealerGroup
            State.Id = State.MyBO.Id

            State.searchDV = State.MyBO.GetNewDataViewRow(State.searchDV, State.Id, State.MyBO)

            Grid.DataSource = State.searchDV

            SetPageAndSelectedIndexFromGuid(State.searchDV, State.Id, Grid, State.PageIndex, State.IsEditMode)

            Grid.AutoGenerateColumns = False
            Grid.Columns(DESCRIPTION_COL_IDX).SortExpression = DealerGroup.DealerGroupSearchDV.COL_DESCRIPTION
            Grid.Columns(CODE_COL_IDX).SortExpression = DealerGroup.DealerGroupSearchDV.COL_CODE

            SortAndBindGrid()
            SetGridControls(Grid, False)

            'Set focus on the Description TextBox for the EditItemIndex row
            SetFocusOnEditableFieldInGrid(Grid, DESCRIPTION_COL_IDX, DESCRIPTION_CONTROL_NAME, Grid.EditIndex)
            PopulateFormFromBO()
        End Sub

        Private Sub PopulateBOFromForm()

            Try
                With State.MyBO
                    .Description = CType(Grid.Rows(Grid.EditIndex).Cells(DESCRIPTION_COL_IDX).FindControl(DESCRIPTION_CONTROL_NAME), TextBox).Text
                    .Code = CType(Grid.Rows(Grid.EditIndex).Cells(CODE_COL_IDX).FindControl(CODE_CONTROL_NAME), TextBox).Text
                    .CompanyGroupId = State.CompanyGroupId
                    Dim AcctingByGroupList As DropDownList = CType(Grid.Rows(Grid.EditIndex).Cells(ACCTING_BY_GROUP_IDX).FindControl(ACCTBYGROUP_CONTROL_NAME), DropDownList)
                    PopulateBOProperty(State.MyBO, "AcctingByGroupId", AcctingByGroupList)

                    Dim UseClientDealerCodeList As DropDownList = CType(Grid.Rows(Grid.EditIndex).Cells(USE_CLIENT_DEALER_CODE_IDX).FindControl(USECLIENTDEALERCODE_CONTROL_NAME), DropDownList)
                    PopulateBOProperty(State.MyBO, "UseClientCodeYNId", UseClientDealerCodeList)

                    Dim AutoRejErrTypeList As DropDownList = CType(Grid.Rows(Grid.EditIndex).Cells(AUTO_REJ_ERR_TYPE_IDX).FindControl(AUTOREJERRTYPE_CONTROL_NAME), DropDownList)
                    PopulateBOProperty(State.MyBO, "AutoRejErrTypeId", AutoRejErrTypeList)

                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        'Private Sub PopulateFormFromBO()

        '    Dim gridRowIdx As Integer = Me.Grid.EditIndex
        '    Try
        '        With Me.State.MyBO
        '            If Not .Description Is Nothing Then
        '                CType(Me.Grid.Rows(gridRowIdx).Cells(Me.DESCRIPTION_COL_IDX).FindControl(Me.DESCRIPTION_CONTROL_NAME), TextBox).Text = .Description
        '            End If
        '            If Not .Code Is Nothing Then
        '                CType(Me.Grid.Rows(gridRowIdx).Cells(Me.CODE_COL_IDX).FindControl(Me.CODE_CONTROL_NAME), TextBox).Text = .Code
        '            End If
        '            Dim AcctingByGroupList As DropDownList = CType(Me.Grid.Rows(gridRowIdx).Cells(Me.ACCTING_BY_GROUP_IDX).FindControl(Me.ACCTBYGROUP_CONTROL_NAME), DropDownList)
        '            Me.BindSelectItem(Me.State.MyBO.AcctingByGroupId.ToString, AcctingByGroupList)
        '        End With
        '    Catch ex As Exception
        '        Me.HandleErrors(ex, Me.ErrController)
        '    End Try

        'End Sub

        Private Sub ReturnFromEditing()

            Grid.EditIndex = NO_ROW_SELECTED_INDEX

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

        Private Sub PopulateAcctingByGroupDropdown(AcctingByGroupList As DropDownList)
            Try
                'Dim yesNoLkL As DataView = LookupListNew.DropdownLookupList("YESNO", ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
                'Me.BindListControlToDataView(AcctingByGroupList, yesNoLkL, "DESCRIPTION", "ID", False)
                Dim yesNoLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())
                AcctingByGroupList.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = False
                                                       })
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub PopulateUseClientDealerCodeDropdown(UseClientDealerCodeList As DropDownList)
            Try
                'Dim yesNoLkL As DataView = LookupListNew.DropdownLookupList("YESNO", ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
                'Me.BindListControlToDataView(UseClientDealerCodeList, yesNoLkL, "DESCRIPTION", "ID", False)
                Dim yesNoLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())
                UseClientDealerCodeList.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub PopulateAutoRejErrTypeDropdown(AutoRejErrTypeList As DropDownList)
            Try
                'Dim autoRejErrLkL As DataView = LookupListNew.DropdownLookupList("AUTO_REJ_ERR_TYPE", ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
                'Me.BindListControlToDataView(AutoRejErrTypeList, autoRejErrLkL, "DESCRIPTION", "ID", False)
                Dim autoRejErrLkL As ListItem() = CommonConfigManager.Current.ListManager.GetList("AUTO_REJ_ERR_TYPE", Thread.CurrentPrincipal.GetLanguageCode())
                AutoRejErrTypeList.Populate(autoRejErrLkL, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub
#End Region

#Region "GridView Related "

        'The Binding Logic is here
        Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                If dvRow IsNot Nothing AndAlso Not State.searchDV.Count > 0 Then
                    If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                        CType(e.Row.Cells(ID_COL_IDX).FindControl(ID_CONTROL_NAME), Label).Text = GetGuidStringFromByteArray(CType(dvRow(DealerGroup.DealerGroupSearchDV.COL_DEALER_GROUP_ID), Byte()))
                        If (State.IsEditMode = True AndAlso State.Id.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(DealerGroup.DealerGroupSearchDV.COL_DEALER_GROUP_ID), Byte())))) Then
                            CType(e.Row.Cells(DESCRIPTION_COL_IDX).FindControl(DESCRIPTION_CONTROL_NAME), TextBox).Text = dvRow(DealerGroup.DealerGroupSearchDV.COL_DESCRIPTION).ToString
                            CType(e.Row.Cells(CODE_COL_IDX).FindControl(CODE_CONTROL_NAME), TextBox).Text = dvRow(DealerGroup.DealerGroupSearchDV.COL_CODE).ToString
                            Dim AcctingByGroupList As DropDownList = CType(e.Row.Cells(CODE_COL_IDX).FindControl(ACCTBYGROUP_CONTROL_NAME), DropDownList)
                            Dim UseClientDealerCodeList As DropDownList = CType(e.Row.Cells(ACCTING_BY_GROUP_IDX).FindControl(USECLIENTDEALERCODE_CONTROL_NAME), DropDownList)
                            Dim AutoRejErrTypeList As DropDownList = CType(e.Row.Cells(AUTO_REJ_ERR_TYPE_IDX).FindControl(AUTOREJERRTYPE_CONTROL_NAME), DropDownList)

                            PopulateAcctingByGroupDropdown(AcctingByGroupList)
                            PopulateUseClientDealerCodeDropdown(UseClientDealerCodeList)
                            PopulateAutoRejErrTypeDropdown(AutoRejErrTypeList)

                            SetSelectedItem(AcctingByGroupList, State.MyBO.AcctingByGroupId)
                            SetSelectedItem(UseClientDealerCodeList, State.MyBO.UseClientCodeYNId)
                            SetSelectedItem(AutoRejErrTypeList, State.MyBO.AutoRejErrTypeId)

                        Else
                            CType(e.Row.Cells(DESCRIPTION_COL_IDX).FindControl(DESCRIPTION_CONTROL_NAME), Label).Text = dvRow(DealerGroup.DealerGroupSearchDV.COL_DESCRIPTION).ToString
                            CType(e.Row.Cells(CODE_COL_IDX).FindControl(CODE_CONTROL_NAME), Label).Text = dvRow(DealerGroup.DealerGroupSearchDV.COL_CODE).ToString
                            CType(e.Row.Cells(ACCTING_BY_GROUP_IDX).FindControl(ACCTBYGROUP_LABEL_CONTROL_NAME), Label).Text = dvRow(DealerGroup.DealerGroupSearchDV.COL_ACCTING_BY_GROUP_DESC).ToString
                            CType(e.Row.Cells(USE_CLIENT_DEALER_CODE_IDX).FindControl(USECLIENTDEALERCODE_CONTROL_NAME), Label).Text = dvRow(DealerGroup.DealerGroupSearchDV.COL_USE_CLIENT_CODE_YNDESC).ToString
                            CType(e.Row.Cells(AUTO_REJ_ERR_TYPE_IDX).FindControl(AUTOREJERRTYPE_CONTROL_NAME), Label).Text = dvRow(DealerGroup.DealerGroupSearchDV.COL_AUTO_REJ_ERR_TYPE_DESC).ToString
                        End If
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanging(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging

            Try
                If (Not (State.IsEditMode)) Then
                    State.PageIndex = e.NewPageIndex
                    Grid.PageIndex = State.PageIndex
                    PopulateGrid()
                    Grid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub RowCommand(source As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Try
                Dim index As Integer

                If (e.CommandName = EDIT_COMMAND) Then

                    index = CInt(e.CommandArgument)

                    'Do the Edit here

                    'Set the IsEditMode flag to TRUE
                    State.IsEditMode = True

                    State.Id = New Guid(CType(Grid.Rows(index).Cells(ID_COL_IDX).FindControl(ID_CONTROL_NAME), Label).Text)
                    State.MyBO = New DealerGroup(State.Id)

                    PopulateGrid()

                    State.PageIndex = Grid.PageIndex

                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(Grid, False)

                    'Set focus on the Description TextBox for the EditItemIndex row
                    SetFocusOnEditableFieldInGrid(Grid, DESCRIPTION_COL_IDX, DESCRIPTION_CONTROL_NAME, index)

                    PopulateFormFromBO()

                    SetButtonsState()

                ElseIf (e.CommandName = DELETE_COMMAND) Then
                    index = CInt(e.CommandArgument)

                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    Grid.SelectedIndex = NO_ROW_SELECTED_INDEX
                    State.Id = New Guid(CType(Grid.Rows(index).Cells(ID_COL_IDX).FindControl(ID_CONTROL_NAME), Label).Text)

                    DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenDeletePromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub
        Private Sub PopulateFormFromBO()

            Dim gridRowIdx As Integer = Grid.EditIndex
            Try
                With State.MyBO

                    If (.Code IsNot Nothing) Then
                        CType(Grid.Rows(gridRowIdx).Cells(CODE_COL_IDX).FindControl(CODE_CONTROL_NAME), TextBox).Text = .Code
                    End If
                    If (.Description IsNot Nothing) Then
                        CType(Grid.Rows(gridRowIdx).Cells(DESCRIPTION_COL_IDX).FindControl(DESCRIPTION_CONTROL_NAME), TextBox).Text = .Description
                    End If
                    CType(Grid.Rows(gridRowIdx).Cells(ID_COL_IDX).FindControl(ID_CONTROL_NAME), Label).Text = .Id.ToString

                    Dim AcctingByGroupList As DropDownList = CType(Grid.Rows(gridRowIdx).Cells(ACCTING_BY_GROUP_IDX).FindControl(ACCTBYGROUP_CONTROL_NAME), DropDownList)
                    PopulateAcctingByGroupDropdown(AcctingByGroupList)
                    SetSelectedItem(AcctingByGroupList, .AcctingByGroupId)

                    Dim UseClientDealerCodeList As DropDownList = CType(Grid.Rows(gridRowIdx).Cells(USE_CLIENT_DEALER_CODE_IDX).FindControl(USECLIENTDEALERCODE_CONTROL_NAME), DropDownList)
                    PopulateUseClientDealerCodeDropdown(UseClientDealerCodeList)
                    'UseClientDealerCodeList.Items.Insert(0, New ListItem(" ", Guid.Empty.ToString))
                    If Not State.MyBO.UseClientCodeYNId.Equals(System.Guid.Empty) Then
                        SetSelectedItem(UseClientDealerCodeList, .UseClientCodeYNId)
                    Else
                        UseClientDealerCodeList.SelectedIndex = 0
                    End If

                    Dim AutoRejErrTypeList As DropDownList = CType(Grid.Rows(gridRowIdx).Cells(AUTO_REJ_ERR_TYPE_IDX).FindControl(AUTOREJERRTYPE_CONTROL_NAME), DropDownList)
                    PopulateAutoRejErrTypeDropdown(AutoRejErrTypeList)
                    'AutoRejErrTypeList.Items.Insert(0, New ListItem(" ", Guid.Empty.ToString))
                    If Not State.MyBO.AutoRejErrTypeId.Equals(System.Guid.Empty) Then
                        SetSelectedItem(AutoRejErrTypeList, .AutoRejErrTypeId)
                    Else
                        AutoRejErrTypeList.SelectedIndex = 0
                    End If
                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub
        Protected Sub RowCreated(sender As Object, e As GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_Sorting(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting

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
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()
            BindBOPropertyToGridHeader(State.MyBO, "Description", Grid.Columns(DESCRIPTION_COL_IDX))
            BindBOPropertyToGridHeader(State.MyBO, "Code", Grid.Columns(CODE_COL_IDX))
            BindBOPropertyToGridHeader(State.MyBO, "AcctingByGroupId", Grid.Columns(ACCTING_BY_GROUP_IDX))
            BindBOPropertyToGridHeader(State.MyBO, "UseClientCodeYNId", Grid.Columns(USE_CLIENT_DEALER_CODE_IDX))
            BindBOPropertyToGridHeader(State.MyBO, "AutoRejErrTypeId", Grid.Columns(AUTO_REJ_ERR_TYPE_IDX))
            ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(grid As GridView, cellPosition As Integer, controlName As String, itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim desc As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(desc)
        End Sub

        Private Sub Grid1_Sorting(sender As Object, e As GridViewSortEventArgs) Handles Grid1.Sorting
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
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region


    End Class

End Namespace

