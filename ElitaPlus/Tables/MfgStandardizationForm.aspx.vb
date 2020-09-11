Option Strict On
Option Explicit On
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Tables

    Partial Class MfgStandardizationForm
        Inherits ElitaPlusSearchPage

        Private Class PageStatus

            Public Sub New()
                pageIndex = 0
                pageCount = 0
            End Sub

        End Class

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

#Region "Page State"

        Class MyState
            ' Public myBO As MfgStandardization = New MfgStandardization
            Public myBO As MfgStandardization
            Public IsNew As Boolean
            Public AddingNewRow As Boolean
            Public IsEditMode As Boolean
            Public IsGridVisible As Boolean
            Public IsAfterSave As Boolean
            Public Cancelling As Boolean
            Public searchDV As DataView = Nothing
            Public Id As Guid
            Public CompanyGroupId As Guid
            Public ManufacturerSearchId As Guid
            'Public CompanyId As Guid
            'Public newMfgStandardizationId As Guid
            Public DescriptionMask As String
            Public PageIndex As Integer = 0
            Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
            Public SortExpression As String = MfgStandardizationDAL.COL_NAME_DESCRIPTION
            Public bnoRow As Boolean = False
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

        'Cols
        Private Const MFGStandardization_ID As Integer = 2
        Private Const MFG_ID As Integer = 3
        Private Const GRID_COL_MFG_ALIAS As Integer = 4
        Private Const GRID_COL_MFG As Integer = 5

        Private Const DESCRIPTION_IN_GRID_CONTROL_NAME As String = "TextBoxGridDescription"
        Private Const MANUFACTURER_LIST_IN_GRID_CONTROL_NAME As String = "moMfgDropDown"

        'Actions
        Private Const ACTION_NONE As String = "ACTION_NONE"
        Private Const ACTION_SAVE As String = "ACTION_SAVE"
        Private Const ACTION_NO_EDIT As String = "ACTION_NO_EDIT"
        Private Const ACTION_EDIT As String = "ACTION_EDIT"
        Private Const ACTION_NEW As String = "ACTION_NEW"

        Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

        Private Const NO_ROW_SELECTED_INDEX As Integer = -1

        Private Const MANUFACTURER_DESCRIPTION_COL_NAME As String = "CODE"


#End Region

#Region "Member Variables"
        Private Shared pageIndex As Integer
        Protected WithEvents moCompanyGroupLabel As System.Web.UI.WebControls.Label
        Protected WithEvents moCompanyGroupColonLabel_NO_TRANSLATE As System.Web.UI.WebControls.Label
        Private Shared pageCount As Integer

        Private moMfgStandardization As BusinessObjectsNew.MfgStandardization
        Private moMfgStandardizationId As String
        Private isReturning As Boolean = False

#End Region

#Region "Properties"

        Public ReadOnly Property IsEditing() As Boolean
            Get
                IsEditing = (Me.moMfgGrid.EditIndex > NO_ROW_SELECTED_INDEX)
            End Get
        End Property


#End Region

#Region "Handlers"

#Region "Handlers_Init"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                moErrorController.Clear_Hide()
                Me.SetStateProperties()
                If Not Page.IsPostBack Then
                    Me.SortDirection = Me.State.SortExpression
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Me.SetDefaultButton(Me.moTxtMfgStandardSearch, Me.moBtnSearch)
                    Me.SetGridItemStyleColor(Me.moMfgGrid)
                    Me.TranslateGridHeader(Me.moMfgGrid)
                    If Me.State.myBO Is Nothing Then
                        Me.State.myBO = New MfgStandardization
                    End If
                    PopulateDropdowns()
                    Me.State.PageIndex = 0
                    SetButtonsState()
                End If
                BindBoPropertiesToGridHeaders()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try
            Me.ShowMissingTranslations(moErrorController)


        End Sub

#End Region

#Region "Handlers_Butons"

        Private Sub SearchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnSearch.Click

            Try
                Me.State.PageIndex = 0
                Me.State.Id = Guid.Empty
                Me.State.IsGridVisible = True
                Me.State.searchDV = Nothing
                PopulateGrid()
                Me.State.PageIndex = moMfgGrid.PageIndex
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try


        End Sub

        Private Sub ClearButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnClearSearch.Click
            ClearSearchCriteria()

        End Sub

        Private Sub NewButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnAdd_WRITE.Click

            Try
                Me.State.IsEditMode = True
                Me.State.IsGridVisible = True
                Me.State.AddingNewRow = True
                AddNew()
                Me.SetGridControls(Me.moMfgGrid, False)
                SetButtonsState()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try

        End Sub

        Private Sub SaveButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnSave_WRITE.Click

            Try
                PopulateBOFromForm()
                If (Me.State.myBO.IsDirty) Then
                    Me.State.myBO.Save()
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

        Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnCancel.Click

            Try
                Me.moMfgGrid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                Me.State.Cancelling = True
                If (Me.State.AddingNewRow) Then
                    Me.State.AddingNewRow = False
                    Me.State.searchDV = Nothing
                End If
                ReturnFromEditing()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try


        End Sub
        Private Sub ClearSearchCriteria()

            Try
                moTxtMfgStandardSearch.Text = String.Empty
                moDropdownMfg.SelectedIndex = 0

                'Update Page State
                With Me.State
                    .DescriptionMask = moTxtMfgStandardSearch.Text
                    .ManufacturerSearchId = Guid.Empty
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try


        End Sub

#End Region

#Region "Handlers_Grid"

        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(ByVal value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

        Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moMfgGrid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                If Not dvRow Is Nothing And Not Me.State.bnoRow Then
                    If (itemType = ListItemType.Item OrElse
                        itemType = ListItemType.AlternatingItem OrElse
                        itemType = ListItemType.SelectedItem) Then

                        If (Me.State.IsEditMode = True _
                            AndAlso Me.State.Id.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(MfgStandardization.MfgStandardizationSearchDV.COL_MFG_STANDARDIZATION_ID), Byte())))) Then

                            e.Row.Cells(Me.MFGStandardization_ID).Text = GetGuidStringFromByteArray(CType(dvRow(MfgStandardization.MfgStandardizationSearchDV.COL_MFG_STANDARDIZATION_ID), Byte()))
                            CType(e.Row.Cells(Me.GRID_COL_MFG_ALIAS).FindControl(DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox).Text = dvRow(MfgStandardization.MfgStandardizationSearchDV.COL_MFG_ALIAS).ToString

                            'Me.BindListControlToDataView(CType(e.Row.Cells(Me.GRID_COL_MFG).FindControl(MANUFACTURER_LIST_IN_GRID_CONTROL_NAME), DropDownList), LookupListNew.GetManufacturerLookupList(Me.State.CompanyGroupId), MANUFACTURER_DESCRIPTION_COL_NAME, , True)
                            Dim listcontext As ListContext = New ListContext()
                            listcontext.CompanyGroupId = Me.State.CompanyGroupId
                            Dim manufacturerLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ManufacturerByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                            CType(e.Row.Cells(Me.GRID_COL_MFG).FindControl(MANUFACTURER_LIST_IN_GRID_CONTROL_NAME), DropDownList).Populate(manufacturerLkl, New PopulateOptions() With
                            {
                             .AddBlankItem = True,
                             .TextFunc = AddressOf .GetCode
                           })
                            Me.SetSelectedItem(CType(e.Row.Cells(Me.GRID_COL_MFG).FindControl(MANUFACTURER_LIST_IN_GRID_CONTROL_NAME), DropDownList), Me.State.myBO.MfgId)

                        Else
                            e.Row.Cells(Me.MFGStandardization_ID).Text = GetGuidStringFromByteArray(CType(dvRow(MfgStandardization.MfgStandardizationSearchDV.COL_MFG_STANDARDIZATION_ID), Byte()))
                            e.Row.Cells(Me.GRID_COL_MFG_ALIAS).Text = dvRow(MfgStandardization.MfgStandardizationSearchDV.COL_MFG_ALIAS).ToString
                            e.Row.Cells(Me.GRID_COL_MFG).Text = GetGuidStringFromByteArray(CType(dvRow(MfgStandardization.MfgStandardizationSearchDV.COL_MANUFACTURER_ID), Byte()))
                            e.Row.Cells(Me.GRID_COL_MFG).Text = dvRow(MfgStandardization.MfgStandardizationSearchDV.COL_MFG).ToString

                        End If

                        '    e.Row.Cells(Me.MFGStandardization_ID).Text = GetGuidStringFromByteArray(CType(dvRow(MfgStandardization.MfgStandardizationSearchDV.COL_MFG_STANDARDIZATION_ID), Byte()))
                        '    e.Row.Cells(Me.GRID_COL_MFG_ALIAS).Text = dvRow(MfgStandardization.MfgStandardizationSearchDV.COL_MFG_ALIAS).ToString
                        '    e.Row.Cells(Me.GRID_COL_MFG).Text = GetGuidStringFromByteArray(CType(dvRow(MfgStandardization.MfgStandardizationSearchDV.COL_MANUFACTURER_ID), Byte()))
                        '    e.Row.Cells(Me.GRID_COL_MFG).Text = dvRow(MfgStandardization.MfgStandardizationSearchDV.COL_MFG).ToString

                        'ElseIf (itemType = ListItemType.EditItem) Then
                        '    e.Row.Cells(Me.MFGStandardization_ID).Text = GetGuidStringFromByteArray(CType(dvRow(MfgStandardization.MfgStandardizationSearchDV.COL_MFG_STANDARDIZATION_ID), Byte()))
                        '    CType(e.Row.Cells(Me.GRID_COL_MFG_ALIAS).FindControl(DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox).Text = dvRow(MfgStandardization.MfgStandardizationSearchDV.COL_MFG_ALIAS).ToString

                        '    Me.BindListControlToDataView(CType(e.Row.Cells(Me.GRID_COL_MFG).FindControl(MANUFACTURER_LIST_IN_GRID_CONTROL_NAME), DropDownList), LookupListNew.GetManufacturerLookupList(Me.State.CompanyGroupId), MANUFACTURER_DESCRIPTION_COL_NAME, , True)
                        '    Me.SetSelectedItem(CType(e.Row.Cells(Me.GRID_COL_MFG).FindControl(MANUFACTURER_LIST_IN_GRID_CONTROL_NAME), DropDownList), Me.State.myBO.MfgId)
                    End If
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try

        End Sub

        Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles moMfgGrid.PageIndexChanging

            Try
                If (Not (Me.State.IsEditMode)) Then
                    Me.State.PageIndex = e.NewPageIndex
                    Me.moMfgGrid.PageIndex = Me.State.PageIndex
                    Me.PopulateGrid()
                    Me.moMfgGrid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try

        End Sub

        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                moMfgGrid.PageIndex = NewCurrentPageIndex(moMfgGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try
        End Sub

        Protected Sub ItemBound(ByVal source As Object, ByVal e As GridViewRowEventArgs) Handles moMfgGrid.RowDataBound

            BaseItemBound(source, e)

        End Sub

        Protected Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Try
                Dim index As Integer

                If (e.CommandName = Me.EDIT_COMMAND_NAME) Then
                    'Do the Edit here
                    index = CInt(e.CommandArgument)
                    'Set the IsEditMode flag to TRUE
                    Me.State.IsEditMode = True
                    'Me.moMfgGrid.Rows(index)
                    Me.State.Id = New Guid(Me.moMfgGrid.Rows(index).Cells(Me.MFGStandardization_ID).Text)

                    Me.State.myBO = New MfgStandardization(Me.State.Id)

                    Me.PopulateGrid()

                    Me.State.PageIndex = moMfgGrid.PageIndex

                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(Me.moMfgGrid, False)

                    'Set focus on the Dealer dropdown list for the EditItemIndex row
                    '?needed?'Me.SetFocusOnEditableFieldInGrid(Me.moMfgGrid, Me.DEALER_COL_IDX, index)

                    Me.SetButtonsState()

                ElseIf (e.CommandName = Me.DELETE_COMMAND_NAME) Then
                    'Do the delete here
                    index = CInt(e.CommandArgument)
                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    moMfgGrid.SelectedIndex = Me.NO_ROW_SELECTED_INDEX

                    'Save the Id in the Session
                    Me.State.Id = New Guid(Me.moMfgGrid.Rows(index).Cells(Me.MFGStandardization_ID).Text)
                    Me.State.myBO = New MfgStandardization(Me.State.Id)

                    Try
                        Me.State.myBO.Delete()
                        'Call the Save() method in the Business Object here
                        Me.State.myBO.Save()
                    Catch ex As Exception
                        Me.State.myBO.RejectChanges()
                        Throw ex
                    End Try

                    Me.State.PageIndex = moMfgGrid.PageIndex

                    'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
                    Me.State.IsAfterSave = True

                    Me.State.searchDV = Nothing
                    PopulateGrid()
                    Me.State.PageIndex = moMfgGrid.PageIndex
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try

        End Sub
        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles moMfgGrid.Sorting
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
                Me.State.SortExpression = Me.SortDirection
                Me.State.PageIndex = 0
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try
        End Sub

        Protected Sub ItemCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try
        End Sub

        ''!NEW
        Protected Sub BindBoPropertiesToGridHeaders()
            Me.BindBOPropertyToGridHeader(Me.State.myBO, "description", Me.moMfgGrid.Columns(Me.GRID_COL_MFG_ALIAS))
            Me.BindBOPropertyToGridHeader(Me.State.myBO, "MfgId", Me.moMfgGrid.Columns(Me.GRID_COL_MFG))
            Me.ClearGridViewHeadersAndLabelsErrSign()
        End Sub
#End Region


#End Region

#Region "Private Methods"

        Private Sub SetButtonsState()

            If (Me.State.IsEditMode) Then
                ControlMgr.SetVisibleControl(Me, moBtnSave_WRITE, True)
                ControlMgr.SetVisibleControl(Me, moBtnCancel, True)
                ControlMgr.SetVisibleControl(Me, moBtnAdd_WRITE, False)
                ControlMgr.SetEnableControl(Me, moBtnSearch, False)
                ControlMgr.SetEnableControl(Me, moBtnClearSearch, False)
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
                Me.MenuEnabled = True
                If (Me.cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, True)
                End If
            End If

        End Sub
        ''!NEW
        Private Sub SetStateProperties()

            Me.State.DescriptionMask = moTxtMfgStandardSearch.Text

            If (Not moDropdownMfg.SelectedItem Is Nothing AndAlso moDropdownMfg.SelectedItem.Value <> Me.NOTHING_SELECTED_TEXT) Then
                Me.State.ManufacturerSearchId = Me.GetGuidFromString(moDropdownMfg.SelectedItem.Value)
            Else
                Me.State.ManufacturerSearchId = Guid.Empty
            End If
            Me.State.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

        End Sub
        Private Sub SortAndBindGrid()
            Me.State.PageIndex = Me.moMfgGrid.PageIndex
            Dim dv As New DataView

            If (Me.State.searchDV.Count = 0) Then

                dv = MfgStandardization.GetMfgAliasList(Me.State.DescriptionMask, State.ManufacturerSearchId, State.CompanyGroupId)

                Me.State.bnoRow = True
                dv = MfgStandardization.getEmptyList(dv)
                moMfgGrid.DataSource = dv
                moMfgGrid.DataBind()
                moMfgGrid.Rows(0).Visible = False
            Else
                Me.State.bnoRow = False
                Me.moMfgGrid.Enabled = True
                Me.moMfgGrid.DataSource = Me.State.searchDV
                HighLightSortColumn(moMfgGrid, Me.SortDirection)
                Me.moMfgGrid.DataBind()
            End If

            If Not moMfgGrid.BottomPagerRow.Visible Then moMfgGrid.BottomPagerRow.Visible = True
            ControlMgr.SetVisibleControl(Me, moMfgGrid, Me.State.IsGridVisible)

            ControlMgr.SetVisibleControl(Me, trPageSize, Me.moMfgGrid.Visible)

            Session("recCount") = Me.State.searchDV.Count

            If Me.moMfgGrid.Visible Then
                If (Me.State.AddingNewRow) Then
                    Me.lblRecordCount.Text = (Me.State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, moMfgGrid)
        End Sub
        ''!NEW
        Private Sub AddNew()

            Me.State.searchDV = GetGridDataView()

            Me.State.myBO = New MfgStandardization
            Me.State.Id = Me.State.myBO.Id

            Me.State.searchDV = Me.State.myBO.GetNewDataViewRow(Me.State.searchDV, Me.State.Id)

            moMfgGrid.DataSource = Me.State.searchDV
            SetGridControls(Me.moMfgGrid, False)
            Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.moMfgGrid, Me.State.PageIndex, Me.State.IsEditMode)

            Me.moMfgGrid.AutoGenerateColumns = False
            Me.moMfgGrid.Columns(Me.GRID_COL_MFG).SortExpression = MfgStandardization.MfgStandardizationSearchDV.COL_MFG
            Me.moMfgGrid.Columns(Me.GRID_COL_MFG_ALIAS).SortExpression = MfgStandardization.MfgStandardizationSearchDV.COL_MFG_ALIAS

            Me.SortAndBindGrid()

            'Set focus on the Dealer dropdown list for the EditItemIndex row
            ' check it for later if needed...
            'Me.SetFocusOnEditableFieldInGrid(Me.moMfgGrid, Me.GRID_COL_MFG, Me.moMfgGrid.EditItemIndex)

        End Sub

        Private Sub moBtnDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles moBtnDelete.Click

        End Sub

        Private Sub moBtnDelete_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles moBtnDelete.Command

        End Sub

        ''!OLD
        ''Private Sub PopulateMfgDropdown()
        ''    Me.BindListControlToDataView(moDropdownMfg, LookupListNew.GetManufacturerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.id))
        ''End Sub
        'Used to populate the dropdowns within the Search panel
        ''!NEW
        Private Sub PopulateDropdowns()

            'Me.BindListControlToDataView(Me.moDropdownMfg, LookupListNew.GetManufacturerLookupList(Me.State.CompanyGroupId), MANUFACTURER_DESCRIPTION_COL_NAME, , True)
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyGroupId = Me.State.CompanyGroupId
            Dim manufacturerLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ManufacturerByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            Me.moDropdownMfg.Populate(manufacturerLkl, New PopulateOptions() With
            {
              .AddBlankItem = True,
              .TextFunc = AddressOf .GetCode
            })
            Me.SetSelectedItem(Me.moDropdownMfg, Me.State.ManufacturerSearchId)

        End Sub
        ''!OLD
        ''Private Sub PopulateMfgStandardizationGrid(Optional ByVal oAction As String = ACTION_NONE)

        ''    Dim oDataView As DataView
        ''    Try
        ''        'oDataView = GetDV()
        ''        If Me.State.searchDV Is Nothing Then
        ''            Me.State.searchDV = GetDV()
        ''            'Ticket # 748479 - Search grids in Tables tab should not show pop-up message when number of retrieved record is over 1,000
        ''            'If Not IsNewMfgStandardization Then Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
        ''        End If
        ''        If Me.MfgStandardizationId Is Nothing Or Me.MfgStandardizationId = "" Then MfgStandardizationId = Guid.Empty.ToString
        ''        BasePopulateGrid(moMfgGrid, Me.State.searchDV, Me.GetGuidFromString(MfgStandardizationId), oAction)
        ''        If oAction = "ACTION_NEW" Then
        ''            Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.newMfgStandardizationId, Me.moMfgGrid, Me.moMfgGrid.CurrentPageIndex, True)
        ''        ElseIf oAction = Me.ACTION_SAVE Then
        ''            Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.newMfgStandardizationId, Me.moMfgGrid, Me.moMfgGrid.CurrentPageIndex)
        ''        End If
        ''        Me.State.searchDV.Sort = Me.State.SortExpression
        ''        Me.moMfgGrid.AutoGenerateColumns = False
        ''        Me.moMfgGrid.Columns(Me.GRID_COL_MFG_ALIAS).SortExpression = BusinessObjectsNew.MfgStandardization.COL_NAME_DESCRIPTION
        ''        Me.moMfgGrid.Columns(Me.GRID_COL_MFG).SortExpression = BusinessObjectsNew.MfgStandardization.MfgStandardizationSearchDV.COL_MFG
        ''        Me.moMfgGrid.Columns(Me.MFGStandardization_ID).SortExpression = BusinessObjectsNew.MfgStandardization.MfgStandardizationSearchDV.COL_MFG_STANDARDIZATION_ID
        ''        HighLightSortColumn(moMfgGrid, Me.State.SortExpression)
        ''        ControlMgr.SetVisibleControl(Me, trPageSize, Me.moMfgGrid.Visible)
        ''        If oAction <> "ACTION_NEW" Then
        ''            Me.moMfgGrid.DataSource = Me.State.searchDV
        ''            Me.moMfgGrid.DataBind()
        ''            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, moMfgGrid)
        ''        Else
        ''            Dim cbMfg As DropDownList = CType(moMfgGrid.Items(moMfgGrid.EditItemIndex).Cells(GRID_COL_MFG).Controls(1), DropDownList)
        ''            Me.BindListControlToDataView(cbMfg, LookupListNew.GetManufacturerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.id))
        ''        End If

        ''        If oAction = Me.ACTION_EDIT Then
        ''            Dim cbMfg As DropDownList = CType(moMfgGrid.Items(moMfgGrid.EditItemIndex).Cells(GRID_COL_MFG).Controls(1), DropDownList)
        ''            Me.BindListControlToDataView(cbMfg, LookupListNew.GetManufacturerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.id))
        ''            cbMfg.SelectedValue = New Guid(CType((Me.State.searchDV(moMfgGrid.EditItemIndex)("manufacturer_id")), Byte())).ToString
        ''        End If

        ''        Session("recCount") = Me.State.searchDV.Count

        ''        If Me.State.searchDV.Count > 0 Then
        ''            If Me.moMfgGrid.Visible Then
        ''                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        ''            End If
        ''        Else
        ''            If Me.moMfgGrid.Visible Then
        ''                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        ''            End If
        ''        End If

        ''        moMfgGrid.Columns(0).Visible = moMfgGrid.EditItemIndex = -1
        ''        moMfgGrid.Columns(1).Visible = moMfgGrid.EditItemIndex = -1

        ''    Catch ex As Exception
        ''        Me.HandleErrors(ex, Me.moErrorController)
        ''    End Try

        ''End Sub
        ''!NEW
        Private Sub PopulateGrid()

            Try
                'Refresh the DataView and Call SetPageAndSelectedIndexFromGuid() to go to the Page 
                'where the most recently saved Record exists in the DataView
                If (Me.State.searchDV Is Nothing) Then
                    Me.State.searchDV = GetGridDataView()
                End If

                Me.State.searchDV.Sort = Me.State.SortExpression
                If (Me.State.IsAfterSave) Then
                    Me.State.IsAfterSave = False
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.moMfgGrid, Me.State.PageIndex)
                ElseIf (Me.State.IsEditMode) Then
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.moMfgGrid, Me.State.PageIndex, Me.State.IsEditMode)
                Else
                    'In a Delete scenario...
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.moMfgGrid, Me.State.PageIndex, Me.State.IsEditMode)
                End If

                Me.moMfgGrid.AutoGenerateColumns = False
                Me.moMfgGrid.Columns(Me.GRID_COL_MFG).SortExpression = MfgStandardization.MfgStandardizationSearchDV.COL_MFG
                Me.moMfgGrid.Columns(Me.GRID_COL_MFG_ALIAS).SortExpression = MfgStandardization.MfgStandardizationSearchDV.COL_MFG_ALIAS

                Me.SortAndBindGrid()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try

        End Sub
        ''!NEW
        Private Function GetGridDataView() As DataView

            With State
                Return (MfgStandardization.GetMfgAliasList(.DescriptionMask, .ManufacturerSearchId, .CompanyGroupId))
            End With

        End Function
        Public Overrides Sub BaseSetButtonsState(ByVal bIsEdit As Boolean)
            'SetButtonsState(bIsEdit)
        End Sub

        ''!OLD
        ''Private Sub PopulateBOFromForm(ByVal oMfgStandardization As BusinessObjectsNew.MfgStandardization)

        ''    Try
        ''        With oMfgStandardization
        ''            .MfgId = Me.GetSelectedGridDropItem(moMfgGrid, GRID_COL_Mfg)
        ''            .Description = Me.GetGridText(moMfgGrid, moMfgGrid.EditItemIndex, GRID_COL_MFG_ALIAS)
        ''            .CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.id
        ''        End With
        ''    Catch ex As Exception
        ''        Me.HandleErrors(ex, Me.moErrorController)
        ''    End Try

        ''End Sub
        Private Sub PopulateBOFromForm()
            ''!NEW - check the description check box !!!!
            Try
                With Me.State.myBO
                    .Description = Me.GetGridText(moMfgGrid, moMfgGrid.EditIndex, GRID_COL_MFG_ALIAS)
                    '.Description = CType(Me.moMfgGrid.Items(Me.moMfgGrid.EditItemIndex).Cells(Me.GRID_COL_MFG_ALIAS).FindControl(DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox).Text
                    .MfgId = Me.GetSelectedItem(CType(moMfgGrid.Rows(moMfgGrid.EditIndex).Cells(Me.GRID_COL_MFG).FindControl(MANUFACTURER_LIST_IN_GRID_CONTROL_NAME), DropDownList))
                    .CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try

        End Sub
        Private Sub ReturnFromEditing()

            moMfgGrid.EditIndex = NO_ROW_SELECTED_INDEX

            If Me.moMfgGrid.PageCount = 0 Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, moMfgGrid, False)
            Else
                ControlMgr.SetVisibleControl(Me, moMfgGrid, True)
            End If
            SetGridControls(moMfgGrid, True)
            Me.State.IsEditMode = False
            Me.PopulateGrid()
            Me.State.PageIndex = moMfgGrid.PageIndex
            SetButtonsState()

        End Sub


#End Region

#Region "Overrides"

        ''Public Overrides Sub AddNewBoRow(ByVal oDataView As DataView)

        ''    Dim oId As Guid = Guid.NewGuid

        ''    Me.BaseAddNewGridRow(moMfgGrid, oDataView, oId)

        ''    Me.State.newMfgStandardizationId = oId


        ''End Sub

#End Region

    End Class

End Namespace
