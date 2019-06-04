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
                IsEditing = (Me.Grid.EditIndex > NO_ROW_SELECTED_INDEX)
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
        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

        Private Sub SearchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchButton.Click
            Try
                Me.State.PageIndex = 0
                Me.State.Id = Guid.Empty
                Me.State.IsGridVisible = True
                Me.State.searchDV = Nothing
                PopulateGrid()
                Me.State.PageIndex = Grid.PageIndex
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub ClearButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearButton.Click

            Try
                ClearSearchCriteria()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ClearSearchCriteria()

            Try
                SearchDescriptionTextBox.Text = String.Empty
                SearchCodeTextBox.Text = String.Empty

                'Update Page State
                With Me.State
                    .DescriptionMask = SearchDescriptionTextBox.Text
                    .CodeMask = SearchCodeTextBox.Text
                End With

                ReturnFromEditing()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub SaveButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton_WRITE.Click
            Try
                PopulateBOFromForm()
                If (Me.State.MyBO.IsDirty) Then
                    Me.State.MyBO.Save()
                    Me.State.IsAfterSave = True
                    Me.State.AddingNewRow = False
                    'Me.AddInfoMsg(Me.MSG_RECORD_SAVED_OK)
                    Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                    Me.State.searchDV = Nothing
                    Me.ReturnFromEditing()
                Else
                    'Me.AddInfoMsg(Me.MSG_RECORD_NOT_SAVED)
                    Me.MasterPage.MessageController.AddWarning(Message.MSG_RECORD_NOT_SAVED)
                    Me.ReturnFromEditing()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

#End Region

#Region "Private Methods"
        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(Me.PAGETITLE)
            End If
        End Sub
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            'Put user code to initialize the page here
            Try
                Me.SetFormTab(Me.PAGETAB)
                Me.SetFormTitle(Me.PAGETITLE)

                Me.MasterPage.MessageController.Clear_Hide()
                Me.SetStateProperties()

                Me.MasterPage.MessageController.Clear()
                Me.MasterPage.UsePageTabTitleInBreadCrum = False
                Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(Me.PAGETAB)
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(Me.PAGETITLE)
                Me.UpdateBreadCrum()

                If Not Page.IsPostBack Then
                    Me.SetDefaultButton(Me.SearchCodeTextBox, Me.SearchButton)
                    Me.SetDefaultButton(Me.SearchDescriptionTextBox, Me.SearchButton)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Me.SetGridItemStyleColor(Me.Grid)
                    Me.State.PageIndex = 0
                    SetButtonsState()
                    If Me.State.MyBO Is Nothing Then
                        Me.State.MyBO = New DealerGroup
                    End If
                    Me.TranslateGridHeader(Me.Grid)
                    Me.TranslateGridControls(Me.Grid)
                Else
                    CheckIfComingFromDeleteConfirm()
                End If
                BindBoPropertiesToGridHeaders()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
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
            Me.State.MyBO = New DealerGroup(Me.State.Id)
            Try
                Me.State.MyBO.Delete()
                'Call the Save() method in the DealerGroup Business Object here
                Me.State.MyBO.Save()
                'Me.AddInfoMsg(Me.MSG_RECORD_DELETED_OK)
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

        Private Sub PopulateGrid()

            Try
                'Refresh the DataView and Call SetPageAndSelectedIndexFromGuid() to go to the Page 
                'where the most recently saved Record exists in the DataView

                If (Me.State.searchDV Is Nothing) Then
                    Me.State.searchDV = GetGridDataView()
                    '  Me.State.searchDV.Sort = Me.Grid.DataMember
                End If
                'Ticket # 748479 - Search grids in Tables tab should not show pop-up message when number of retrieved record is over 1,000
                'Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
                Me.State.searchDV.Sort = Me.State.SortExpression

                If (Me.State.IsAfterSave) Then
                    Me.State.IsAfterSave = False
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.Grid, Me.State.PageIndex)
                ElseIf (Me.State.IsEditMode) Then
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
                Else
                    'In a Delete scenario...
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
                End If
                Me.Grid.AutoGenerateColumns = False
                'Me.Grid.Columns(Me.DESCRIPTION_COL_IDX).SortExpression = DealerGroup.DealerGroupSearchDV.COL_DESCRIPTION
                'Me.Grid.Columns(Me.CODE_COL_IDX).SortExpression = DealerGroup.DealerGroupSearchDV.COL_CODE

                SortAndBindGrid()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub PopulateGridWithNoSort()

            Try
                'Refresh the DataView and Call SetPageAndSelectedIndexFromGuid() to go to the Page 
                'where the most recently saved Record exists in the DataView

                If (Me.State.searchDV Is Nothing) Then
                    Me.State.searchDV = GetGridDataView()
                    '  Me.State.searchDV.Sort = Me.Grid.DataMember
                End If
                'Ticket # 748479 - Search grids in Tables tab should not show pop-up message when number of retrieved record is over 1,000
                'Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
                ' Me.State.searchDV.Sort = Me.State.SortExpression

                If (Me.State.IsAfterSave) Then
                    Me.State.IsAfterSave = False
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.Grid, Me.State.PageIndex)
                ElseIf (Me.State.IsEditMode) Then
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
                Else
                    'In a Delete scenario...
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
                End If
                Me.Grid.AutoGenerateColumns = False
                'Me.Grid.Columns(Me.DESCRIPTION_COL_IDX).SortExpression = DealerGroup.DealerGroupSearchDV.COL_DESCRIPTION
                'Me.Grid.Columns(Me.CODE_COL_IDX).SortExpression = DealerGroup.DealerGroupSearchDV.COL_CODE

                'SortAndBindGrid()

                Me.State.PageIndex = Me.Grid.PageIndex
                Me.Grid.DataSource = Me.State.searchDV
                Me.Grid.DataBind()

                ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)

                ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

                Session("recCount") = Me.State.searchDV.Count
                ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub


        Private Function GetGridDataView() As DataView

            With State
                Return (DealerGroup.LoadList(.DescriptionMask, .CodeMask))
            End With

        End Function

        Private Sub SetStateProperties()

            Me.State.DescriptionMask = SearchDescriptionTextBox.Text
            Me.State.CodeMask = SearchCodeTextBox.Text
            Me.State.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

        End Sub

        Private Sub SortAndBindGrid()
            Me.State.PageIndex = Me.Grid.PageIndex
            If (Me.State.searchDV.Count = 0) Then
                CreateHeaderForEmptyGrid(Grid1, Me.State.SortExpression)
                ControlMgr.SetVisibleControl(Me, Grid1, Me.State.IsGridVisible)
                ControlMgr.SetVisibleControl(Me, Grid, False)
                ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)
                Grid1.PagerSettings.Visible = True
                If Not Grid1.BottomPagerRow.Visible Then Grid1.BottomPagerRow.Visible = True
            Else
                Me.Grid.DataSource = Me.State.searchDV
                HighLightSortColumn(Grid, Me.State.SortExpression)
                Me.Grid.DataBind()
                Grid.PagerSettings.Visible = True
                ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
                ControlMgr.SetVisibleControl(Me, Grid1, False)
                ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)
                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
            End If

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


        Private Sub AddNew()
            Me.State.searchDV = GetGridDataView()

            Me.State.MyBO = New DealerGroup
            Me.State.Id = Me.State.MyBO.Id

            Me.State.searchDV = Me.State.MyBO.GetNewDataViewRow(Me.State.searchDV, Me.State.Id, Me.State.MyBO)

            Grid.DataSource = Me.State.searchDV

            Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)

            Me.Grid.AutoGenerateColumns = False
            Me.Grid.Columns(Me.DESCRIPTION_COL_IDX).SortExpression = DealerGroup.DealerGroupSearchDV.COL_DESCRIPTION
            Me.Grid.Columns(Me.CODE_COL_IDX).SortExpression = DealerGroup.DealerGroupSearchDV.COL_CODE

            SortAndBindGrid()
            SetGridControls(Me.Grid, False)

            'Set focus on the Description TextBox for the EditItemIndex row
            Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.DESCRIPTION_COL_IDX, Me.DESCRIPTION_CONTROL_NAME, Me.Grid.EditIndex)
            PopulateFormFromBO()
        End Sub

        Private Sub PopulateBOFromForm()

            Try
                With Me.State.MyBO
                    .Description = CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.DESCRIPTION_COL_IDX).FindControl(Me.DESCRIPTION_CONTROL_NAME), TextBox).Text
                    .Code = CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.CODE_COL_IDX).FindControl(Me.CODE_CONTROL_NAME), TextBox).Text
                    .CompanyGroupId = Me.State.CompanyGroupId
                    Dim AcctingByGroupList As DropDownList = CType(Me.Grid.Rows(Grid.EditIndex).Cells(Me.ACCTING_BY_GROUP_IDX).FindControl(Me.ACCTBYGROUP_CONTROL_NAME), DropDownList)
                    Me.PopulateBOProperty(Me.State.MyBO, "AcctingByGroupId", AcctingByGroupList)

                    Dim UseClientDealerCodeList As DropDownList = CType(Me.Grid.Rows(Grid.EditIndex).Cells(Me.USE_CLIENT_DEALER_CODE_IDX).FindControl(Me.USECLIENTDEALERCODE_CONTROL_NAME), DropDownList)
                    Me.PopulateBOProperty(Me.State.MyBO, "UseClientCodeYNId", UseClientDealerCodeList)

                    Dim AutoRejErrTypeList As DropDownList = CType(Me.Grid.Rows(Grid.EditIndex).Cells(Me.AUTO_REJ_ERR_TYPE_IDX).FindControl(Me.AUTOREJERRTYPE_CONTROL_NAME), DropDownList)
                    Me.PopulateBOProperty(Me.State.MyBO, "AutoRejErrTypeId", AutoRejErrTypeList)

                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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

            If Me.Grid.PageCount = 0 Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, Grid, False)
            Else
                ControlMgr.SetVisibleControl(Me, Grid, True)
            End If

            SetGridControls(Grid, True)
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
                    ControlMgr.SetEnableControl(Me, Me.cboPageSize, True)
                End If
            End If

        End Sub

        Private Sub PopulateAcctingByGroupDropdown(ByVal AcctingByGroupList As DropDownList)
            Try
                'Dim yesNoLkL As DataView = LookupListNew.DropdownLookupList("YESNO", ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
                'Me.BindListControlToDataView(AcctingByGroupList, yesNoLkL, "DESCRIPTION", "ID", False)
                Dim yesNoLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())
                AcctingByGroupList.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = False
                                                       })
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub PopulateUseClientDealerCodeDropdown(ByVal UseClientDealerCodeList As DropDownList)
            Try
                'Dim yesNoLkL As DataView = LookupListNew.DropdownLookupList("YESNO", ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
                'Me.BindListControlToDataView(UseClientDealerCodeList, yesNoLkL, "DESCRIPTION", "ID", False)
                Dim yesNoLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())
                UseClientDealerCodeList.Populate(yesNoLkl, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub PopulateAutoRejErrTypeDropdown(ByVal AutoRejErrTypeList As DropDownList)
            Try
                'Dim autoRejErrLkL As DataView = LookupListNew.DropdownLookupList("AUTO_REJ_ERR_TYPE", ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
                'Me.BindListControlToDataView(AutoRejErrTypeList, autoRejErrLkL, "DESCRIPTION", "ID", False)
                Dim autoRejErrLkL As ListItem() = CommonConfigManager.Current.ListManager.GetList("AUTO_REJ_ERR_TYPE", Thread.CurrentPrincipal.GetLanguageCode())
                AutoRejErrTypeList.Populate(autoRejErrLkL, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub
#End Region

#Region "GridView Related "

        'The Binding Logic is here
        Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                If Not dvRow Is Nothing And Not State.searchDV.Count > 0 Then
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        CType(e.Row.Cells(Me.ID_COL_IDX).FindControl(Me.ID_CONTROL_NAME), Label).Text = GetGuidStringFromByteArray(CType(dvRow(DealerGroup.DealerGroupSearchDV.COL_DEALER_GROUP_ID), Byte()))
                        If (Me.State.IsEditMode = True AndAlso Me.State.Id.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(DealerGroup.DealerGroupSearchDV.COL_DEALER_GROUP_ID), Byte())))) Then
                            CType(e.Row.Cells(Me.DESCRIPTION_COL_IDX).FindControl(DESCRIPTION_CONTROL_NAME), TextBox).Text = dvRow(DealerGroup.DealerGroupSearchDV.COL_DESCRIPTION).ToString
                            CType(e.Row.Cells(Me.CODE_COL_IDX).FindControl(CODE_CONTROL_NAME), TextBox).Text = dvRow(DealerGroup.DealerGroupSearchDV.COL_CODE).ToString
                            Dim AcctingByGroupList As DropDownList = CType(e.Row.Cells(Me.CODE_COL_IDX).FindControl(Me.ACCTBYGROUP_CONTROL_NAME), DropDownList)
                            Dim UseClientDealerCodeList As DropDownList = CType(e.Row.Cells(Me.ACCTING_BY_GROUP_IDX).FindControl(Me.USECLIENTDEALERCODE_CONTROL_NAME), DropDownList)
                            Dim AutoRejErrTypeList As DropDownList = CType(e.Row.Cells(Me.AUTO_REJ_ERR_TYPE_IDX).FindControl(Me.AUTOREJERRTYPE_CONTROL_NAME), DropDownList)

                            PopulateAcctingByGroupDropdown(AcctingByGroupList)
                            PopulateUseClientDealerCodeDropdown(UseClientDealerCodeList)
                            PopulateAutoRejErrTypeDropdown(AutoRejErrTypeList)

                            Me.SetSelectedItem(AcctingByGroupList, Me.State.MyBO.AcctingByGroupId)
                            Me.SetSelectedItem(UseClientDealerCodeList, Me.State.MyBO.UseClientCodeYNId)
                            Me.SetSelectedItem(AutoRejErrTypeList, Me.State.MyBO.AutoRejErrTypeId)

                        Else
                            CType(e.Row.Cells(Me.DESCRIPTION_COL_IDX).FindControl(DESCRIPTION_CONTROL_NAME), Label).Text = dvRow(DealerGroup.DealerGroupSearchDV.COL_DESCRIPTION).ToString
                            CType(e.Row.Cells(Me.CODE_COL_IDX).FindControl(CODE_CONTROL_NAME), Label).Text = dvRow(DealerGroup.DealerGroupSearchDV.COL_CODE).ToString
                            CType(e.Row.Cells(Me.ACCTING_BY_GROUP_IDX).FindControl(ACCTBYGROUP_LABEL_CONTROL_NAME), Label).Text = dvRow(DealerGroup.DealerGroupSearchDV.COL_ACCTING_BY_GROUP_DESC).ToString
                            CType(e.Row.Cells(Me.USE_CLIENT_DEALER_CODE_IDX).FindControl(USECLIENTDEALERCODE_CONTROL_NAME), Label).Text = dvRow(DealerGroup.DealerGroupSearchDV.COL_USE_CLIENT_CODE_YNDESC).ToString
                            CType(e.Row.Cells(Me.AUTO_REJ_ERR_TYPE_IDX).FindControl(AUTOREJERRTYPE_CONTROL_NAME), Label).Text = dvRow(DealerGroup.DealerGroupSearchDV.COL_AUTO_REJ_ERR_TYPE_DESC).ToString
                        End If
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanging(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging

            Try
                If (Not (Me.State.IsEditMode)) Then
                    Me.State.PageIndex = e.NewPageIndex
                    Me.Grid.PageIndex = Me.State.PageIndex
                    Me.PopulateGrid()
                    Me.Grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub RowCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Try
                Dim index As Integer

                If (e.CommandName = Me.EDIT_COMMAND) Then

                    index = CInt(e.CommandArgument)

                    'Do the Edit here

                    'Set the IsEditMode flag to TRUE
                    Me.State.IsEditMode = True

                    Me.State.Id = New Guid(CType(Me.Grid.Rows(index).Cells(Me.ID_COL_IDX).FindControl(Me.ID_CONTROL_NAME), Label).Text)
                    Me.State.MyBO = New DealerGroup(Me.State.Id)

                    Me.PopulateGrid()

                    Me.State.PageIndex = Grid.PageIndex

                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(Me.Grid, False)

                    'Set focus on the Description TextBox for the EditItemIndex row
                    Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.DESCRIPTION_COL_IDX, Me.DESCRIPTION_CONTROL_NAME, index)

                    PopulateFormFromBO()

                    Me.SetButtonsState()

                ElseIf (e.CommandName = Me.DELETE_COMMAND) Then
                    index = CInt(e.CommandArgument)

                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    Grid.SelectedIndex = Me.NO_ROW_SELECTED_INDEX
                    Me.State.Id = New Guid(CType(Me.Grid.Rows(index).Cells(Me.ID_COL_IDX).FindControl(Me.ID_CONTROL_NAME), Label).Text)

                    Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenDeletePromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub
        Private Sub PopulateFormFromBO()

            Dim gridRowIdx As Integer = Me.Grid.EditIndex
            Try
                With Me.State.MyBO

                    If (Not .Code Is Nothing) Then
                        CType(Me.Grid.Rows(gridRowIdx).Cells(Me.CODE_COL_IDX).FindControl(CODE_CONTROL_NAME), TextBox).Text = .Code
                    End If
                    If (Not .Description Is Nothing) Then
                        CType(Me.Grid.Rows(gridRowIdx).Cells(Me.DESCRIPTION_COL_IDX).FindControl(DESCRIPTION_CONTROL_NAME), TextBox).Text = .Description
                    End If
                    CType(Me.Grid.Rows(gridRowIdx).Cells(Me.ID_COL_IDX).FindControl(Me.ID_CONTROL_NAME), Label).Text = .Id.ToString

                    Dim AcctingByGroupList As DropDownList = CType(Me.Grid.Rows(gridRowIdx).Cells(Me.ACCTING_BY_GROUP_IDX).FindControl(Me.ACCTBYGROUP_CONTROL_NAME), DropDownList)
                    PopulateAcctingByGroupDropdown(AcctingByGroupList)
                    Me.SetSelectedItem(AcctingByGroupList, .AcctingByGroupId)

                    Dim UseClientDealerCodeList As DropDownList = CType(Me.Grid.Rows(gridRowIdx).Cells(Me.USE_CLIENT_DEALER_CODE_IDX).FindControl(Me.USECLIENTDEALERCODE_CONTROL_NAME), DropDownList)
                    PopulateUseClientDealerCodeDropdown(UseClientDealerCodeList)
                    'UseClientDealerCodeList.Items.Insert(0, New ListItem(" ", Guid.Empty.ToString))
                    If Not Me.State.MyBO.UseClientCodeYNId.Equals(System.Guid.Empty) Then
                        Me.SetSelectedItem(UseClientDealerCodeList, .UseClientCodeYNId)
                    Else
                        UseClientDealerCodeList.SelectedIndex = 0
                    End If

                    Dim AutoRejErrTypeList As DropDownList = CType(Me.Grid.Rows(gridRowIdx).Cells(Me.AUTO_REJ_ERR_TYPE_IDX).FindControl(Me.AUTOREJERRTYPE_CONTROL_NAME), DropDownList)
                    PopulateAutoRejErrTypeDropdown(AutoRejErrTypeList)
                    'AutoRejErrTypeList.Items.Insert(0, New ListItem(" ", Guid.Empty.ToString))
                    If Not Me.State.MyBO.AutoRejErrTypeId.Equals(System.Guid.Empty) Then
                        Me.SetSelectedItem(AutoRejErrTypeList, .AutoRejErrTypeId)
                    Else
                        AutoRejErrTypeList.SelectedIndex = 0
                    End If
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub
        Protected Sub RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_Sorting(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting

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
                Me.State.Id = Guid.Empty
                Me.State.PageIndex = 0

                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "Description", Me.Grid.Columns(Me.DESCRIPTION_COL_IDX))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "Code", Me.Grid.Columns(Me.CODE_COL_IDX))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "AcctingByGroupId", Me.Grid.Columns(Me.ACCTING_BY_GROUP_IDX))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "UseClientCodeYNId", Me.Grid.Columns(Me.USE_CLIENT_DEALER_CODE_IDX))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "AutoRejErrTypeId", Me.Grid.Columns(Me.AUTO_REJ_ERR_TYPE_IDX))
            Me.ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As GridView, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim desc As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(desc)
        End Sub

        Private Sub Grid1_Sorting(sender As Object, e As GridViewSortEventArgs) Handles Grid1.Sorting
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
                Me.State.Id = Guid.Empty
                Me.State.PageIndex = 0

                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region


    End Class

End Namespace

