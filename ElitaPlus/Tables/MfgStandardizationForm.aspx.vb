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

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
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

        Private ReadOnly Property theMfgStandardization() As BusinessObjectsNew.MfgStandardization
            Get
                If IsNewMfgStandardization() = True Then
                    ' For creating, inserting
                    moMfgStandardization = New BusinessObjectsNew.MfgStandardization
                    MfgStandardizationId = moMfgStandardization.Id.ToString
                Else
                    ' For updating, deleting
                    Dim oMfgStandardizationId As New Guid(MfgStandardizationId)
                    moMfgStandardization = New BusinessObjectsNew.MfgStandardization(oMfgStandardizationId)
                End If

                Return moMfgStandardization
            End Get
        End Property
        Private Property MfgStandardizationId() As String
            Get
                If moMfgGrid.SelectedIndex > NO_ITEM_SELECTED_INDEX AndAlso IsNewMfgStandardization() = False Then
                    moMfgStandardizationId = New Guid(CType((State.searchDV(moMfgGrid.SelectedIndex)("mfg_standardization_id")), Byte())).ToString
                End If
                Return moMfgStandardizationId
            End Get
            Set(Value As String)
                If moMfgGrid.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                    SetSelectedGridText(moMfgGrid, MFGStandardization_ID, Value)
                End If
                moMfgStandardizationId = Value
            End Set
        End Property

        Private Property IsNewMfgStandardization() As Boolean
            Get
                Return State.IsNew
            End Get
            Set(Value As Boolean)
                State.IsNew = Value
            End Set
        End Property

        Public ReadOnly Property IsEditing() As Boolean
            Get
                IsEditing = (moMfgGrid.EditIndex > NO_ROW_SELECTED_INDEX)
            End Get
        End Property


#End Region

#Region "Handlers"

#Region "Handlers_Init"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                moErrorController.Clear_Hide()
                SetStateProperties()
                If Not Page.IsPostBack Then
                    SortDirection = State.SortExpression
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    SetDefaultButton(moTxtMfgStandardSearch, moBtnSearch)
                    SetGridItemStyleColor(moMfgGrid)
                    TranslateGridHeader(moMfgGrid)
                    If State.myBO Is Nothing Then
                        State.myBO = New MfgStandardization
                    End If
                    PopulateDropdowns()
                    State.PageIndex = 0
                    SetButtonsState()
                End If
                BindBoPropertiesToGridHeaders()
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
            ShowMissingTranslations(moErrorController)


        End Sub

#End Region

#Region "Handlers_Butons"

        Private Sub SearchButton_Click(sender As System.Object, e As System.EventArgs) Handles moBtnSearch.Click

            Try
                State.PageIndex = 0
                State.Id = Guid.Empty
                State.IsGridVisible = True
                State.searchDV = Nothing
                PopulateGrid()
                State.PageIndex = moMfgGrid.PageIndex
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try


        End Sub

        Private Sub ClearButton_Click(sender As System.Object, e As System.EventArgs) Handles moBtnClearSearch.Click
            ClearSearchCriteria()

        End Sub

        Private Sub NewButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles moBtnAdd_WRITE.Click

            Try
                State.IsEditMode = True
                State.IsGridVisible = True
                State.AddingNewRow = True
                AddNew()
                SetGridControls(moMfgGrid, False)
                SetButtonsState()
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try

        End Sub

        Private Sub SaveButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles moBtnSave_WRITE.Click

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
                HandleErrors(ex, moErrorController)
            End Try


        End Sub

        Private Sub CancelButton_Click(sender As System.Object, e As System.EventArgs) Handles moBtnCancel.Click

            Try
                moMfgGrid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                State.Cancelling = True
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
                moTxtMfgStandardSearch.Text = String.Empty
                moDropdownMfg.SelectedIndex = 0

                'Update Page State
                With State
                    .DescriptionMask = moTxtMfgStandardSearch.Text
                    .ManufacturerSearchId = Guid.Empty
                End With
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try


        End Sub

#End Region

#Region "Handlers_Grid"

        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

        Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moMfgGrid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                If dvRow IsNot Nothing AndAlso Not State.bnoRow Then
                    If (itemType = ListItemType.Item OrElse
                        itemType = ListItemType.AlternatingItem OrElse
                        itemType = ListItemType.SelectedItem) Then

                        If (State.IsEditMode = True _
                            AndAlso State.Id.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(MfgStandardization.MfgStandardizationSearchDV.COL_MFG_STANDARDIZATION_ID), Byte())))) Then

                            e.Row.Cells(MFGStandardization_ID).Text = GetGuidStringFromByteArray(CType(dvRow(MfgStandardization.MfgStandardizationSearchDV.COL_MFG_STANDARDIZATION_ID), Byte()))
                            CType(e.Row.Cells(GRID_COL_MFG_ALIAS).FindControl(DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox).Text = dvRow(MfgStandardization.MfgStandardizationSearchDV.COL_MFG_ALIAS).ToString

                            'Me.BindListControlToDataView(CType(e.Row.Cells(Me.GRID_COL_MFG).FindControl(MANUFACTURER_LIST_IN_GRID_CONTROL_NAME), DropDownList), LookupListNew.GetManufacturerLookupList(Me.State.CompanyGroupId), MANUFACTURER_DESCRIPTION_COL_NAME, , True)
                            Dim listcontext As ListContext = New ListContext()
                            listcontext.CompanyGroupId = State.CompanyGroupId
                            Dim manufacturerLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ManufacturerByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                            CType(e.Row.Cells(GRID_COL_MFG).FindControl(MANUFACTURER_LIST_IN_GRID_CONTROL_NAME), DropDownList).Populate(manufacturerLkl, New PopulateOptions() With
                            {
                             .AddBlankItem = True,
                             .TextFunc = AddressOf .GetCode
                           })
                            SetSelectedItem(CType(e.Row.Cells(GRID_COL_MFG).FindControl(MANUFACTURER_LIST_IN_GRID_CONTROL_NAME), DropDownList), State.myBO.MfgId)

                        Else
                            e.Row.Cells(MFGStandardization_ID).Text = GetGuidStringFromByteArray(CType(dvRow(MfgStandardization.MfgStandardizationSearchDV.COL_MFG_STANDARDIZATION_ID), Byte()))
                            e.Row.Cells(GRID_COL_MFG_ALIAS).Text = dvRow(MfgStandardization.MfgStandardizationSearchDV.COL_MFG_ALIAS).ToString
                            e.Row.Cells(GRID_COL_MFG).Text = GetGuidStringFromByteArray(CType(dvRow(MfgStandardization.MfgStandardizationSearchDV.COL_MANUFACTURER_ID), Byte()))
                            e.Row.Cells(GRID_COL_MFG).Text = dvRow(MfgStandardization.MfgStandardizationSearchDV.COL_MFG).ToString

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
                HandleErrors(ex, moErrorController)
            End Try

        End Sub

        Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles moMfgGrid.PageIndexChanging

            Try
                If (Not (State.IsEditMode)) Then
                    State.PageIndex = e.NewPageIndex
                    moMfgGrid.PageIndex = State.PageIndex
                    PopulateGrid()
                    moMfgGrid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try

        End Sub

        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                moMfgGrid.PageIndex = NewCurrentPageIndex(moMfgGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Protected Sub ItemBound(source As Object, e As GridViewRowEventArgs) Handles moMfgGrid.RowDataBound

            BaseItemBound(source, e)

        End Sub

        Protected Sub ItemCommand(source As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Try
                Dim index As Integer

                If (e.CommandName = EDIT_COMMAND_NAME) Then
                    'Do the Edit here
                    index = CInt(e.CommandArgument)
                    'Set the IsEditMode flag to TRUE
                    State.IsEditMode = True
                    'Me.moMfgGrid.Rows(index)
                    State.Id = New Guid(moMfgGrid.Rows(index).Cells(MFGStandardization_ID).Text)

                    State.myBO = New MfgStandardization(State.Id)

                    PopulateGrid()

                    State.PageIndex = moMfgGrid.PageIndex

                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(moMfgGrid, False)

                    'Set focus on the Dealer dropdown list for the EditItemIndex row
                    '?needed?'Me.SetFocusOnEditableFieldInGrid(Me.moMfgGrid, Me.DEALER_COL_IDX, index)

                    SetButtonsState()

                ElseIf (e.CommandName = DELETE_COMMAND_NAME) Then
                    'Do the delete here
                    index = CInt(e.CommandArgument)
                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    moMfgGrid.SelectedIndex = NO_ROW_SELECTED_INDEX

                    'Save the Id in the Session
                    State.Id = New Guid(moMfgGrid.Rows(index).Cells(MFGStandardization_ID).Text)
                    State.myBO = New MfgStandardization(State.Id)

                    Try
                        State.myBO.Delete()
                        'Call the Save() method in the Business Object here
                        State.myBO.Save()
                    Catch ex As Exception
                        State.myBO.RejectChanges()
                        Throw ex
                    End Try

                    State.PageIndex = moMfgGrid.PageIndex

                    'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
                    State.IsAfterSave = True

                    State.searchDV = Nothing
                    PopulateGrid()
                    State.PageIndex = moMfgGrid.PageIndex
                End If

            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try

        End Sub
        Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles moMfgGrid.Sorting
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
                State.SortExpression = SortDirection
                State.PageIndex = 0
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Protected Sub ItemCreated(sender As Object, e As GridViewRowEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        ''!NEW
        Protected Sub BindBoPropertiesToGridHeaders()
            BindBOPropertyToGridHeader(State.myBO, "description", moMfgGrid.Columns(GRID_COL_MFG_ALIAS))
            BindBOPropertyToGridHeader(State.myBO, "MfgId", moMfgGrid.Columns(GRID_COL_MFG))
            ClearGridViewHeadersAndLabelsErrSign()
        End Sub
#End Region


#End Region

#Region "Private Methods"

        Private Sub SetButtonsState()

            If (State.IsEditMode) Then
                ControlMgr.SetVisibleControl(Me, moBtnSave_WRITE, True)
                ControlMgr.SetVisibleControl(Me, moBtnCancel, True)
                ControlMgr.SetVisibleControl(Me, moBtnAdd_WRITE, False)
                ControlMgr.SetEnableControl(Me, moBtnSearch, False)
                ControlMgr.SetEnableControl(Me, moBtnClearSearch, False)
                MenuEnabled = False
                If (cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, False)
                End If
            Else
                ControlMgr.SetVisibleControl(Me, moBtnSave_WRITE, False)
                ControlMgr.SetVisibleControl(Me, moBtnCancel, False)
                ControlMgr.SetVisibleControl(Me, moBtnAdd_WRITE, True)
                ControlMgr.SetEnableControl(Me, moBtnSearch, True)
                ControlMgr.SetEnableControl(Me, moBtnClearSearch, True)
                MenuEnabled = True
                If (cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, True)
                End If
            End If

        End Sub
        ''!NEW
        Private Sub SetStateProperties()

            State.DescriptionMask = moTxtMfgStandardSearch.Text

            If (moDropdownMfg.SelectedItem IsNot Nothing AndAlso moDropdownMfg.SelectedItem.Value <> NOTHING_SELECTED_TEXT) Then
                State.ManufacturerSearchId = GetGuidFromString(moDropdownMfg.SelectedItem.Value)
            Else
                State.ManufacturerSearchId = Guid.Empty
            End If
            State.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

        End Sub
        Private Sub SortAndBindGrid()
            State.PageIndex = moMfgGrid.PageIndex
            Dim dv As New DataView

            If (State.searchDV.Count = 0) Then

                dv = MfgStandardization.GetMfgAliasList(State.DescriptionMask, State.ManufacturerSearchId, State.CompanyGroupId)

                State.bnoRow = True
                dv = MfgStandardization.getEmptyList(dv)
                moMfgGrid.DataSource = dv
                moMfgGrid.DataBind()
                moMfgGrid.Rows(0).Visible = False
            Else
                State.bnoRow = False
                moMfgGrid.Enabled = True
                moMfgGrid.DataSource = State.searchDV
                HighLightSortColumn(moMfgGrid, SortDirection)
                moMfgGrid.DataBind()
            End If

            If Not moMfgGrid.BottomPagerRow.Visible Then moMfgGrid.BottomPagerRow.Visible = True
            ControlMgr.SetVisibleControl(Me, moMfgGrid, State.IsGridVisible)

            ControlMgr.SetVisibleControl(Me, trPageSize, moMfgGrid.Visible)

            Session("recCount") = State.searchDV.Count

            If moMfgGrid.Visible Then
                If (State.AddingNewRow) Then
                    lblRecordCount.Text = (State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, moMfgGrid)
        End Sub
        ''!NEW
        Private Sub AddNew()

            State.searchDV = GetGridDataView()

            State.myBO = New MfgStandardization
            State.Id = State.myBO.Id

            State.searchDV = State.myBO.GetNewDataViewRow(State.searchDV, State.Id)

            moMfgGrid.DataSource = State.searchDV
            SetGridControls(moMfgGrid, False)
            SetPageAndSelectedIndexFromGuid(State.searchDV, State.Id, moMfgGrid, State.PageIndex, State.IsEditMode)

            moMfgGrid.AutoGenerateColumns = False
            moMfgGrid.Columns(GRID_COL_MFG).SortExpression = MfgStandardization.MfgStandardizationSearchDV.COL_MFG
            moMfgGrid.Columns(GRID_COL_MFG_ALIAS).SortExpression = MfgStandardization.MfgStandardizationSearchDV.COL_MFG_ALIAS

            SortAndBindGrid()

            'Set focus on the Dealer dropdown list for the EditItemIndex row
            ' check it for later if needed...
            'Me.SetFocusOnEditableFieldInGrid(Me.moMfgGrid, Me.GRID_COL_MFG, Me.moMfgGrid.EditItemIndex)

        End Sub

        Private Sub moBtnDelete_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles moBtnDelete.Click

        End Sub

        Private Sub moBtnDelete_Command(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs) Handles moBtnDelete.Command

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
            listcontext.CompanyGroupId = State.CompanyGroupId
            Dim manufacturerLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ManufacturerByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            moDropdownMfg.Populate(manufacturerLkl, New PopulateOptions() With
            {
              .AddBlankItem = True,
              .TextFunc = AddressOf .GetCode
            })
            SetSelectedItem(moDropdownMfg, State.ManufacturerSearchId)

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
                If (State.searchDV Is Nothing) Then
                    State.searchDV = GetGridDataView()
                End If

                State.searchDV.Sort = State.SortExpression
                If (State.IsAfterSave) Then
                    State.IsAfterSave = False
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.Id, moMfgGrid, State.PageIndex)
                ElseIf (State.IsEditMode) Then
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.Id, moMfgGrid, State.PageIndex, State.IsEditMode)
                Else
                    'In a Delete scenario...
                    SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, moMfgGrid, State.PageIndex, State.IsEditMode)
                End If

                moMfgGrid.AutoGenerateColumns = False
                moMfgGrid.Columns(GRID_COL_MFG).SortExpression = MfgStandardization.MfgStandardizationSearchDV.COL_MFG
                moMfgGrid.Columns(GRID_COL_MFG_ALIAS).SortExpression = MfgStandardization.MfgStandardizationSearchDV.COL_MFG_ALIAS

                SortAndBindGrid()

            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try

        End Sub
        ''!NEW
        Private Function GetGridDataView() As DataView

            With State
                Return (MfgStandardization.GetMfgAliasList(.DescriptionMask, .ManufacturerSearchId, .CompanyGroupId))
            End With

        End Function
        Public Overrides Sub BaseSetButtonsState(bIsEdit As Boolean)
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
                With State.myBO
                    .Description = GetGridText(moMfgGrid, moMfgGrid.EditIndex, GRID_COL_MFG_ALIAS)
                    '.Description = CType(Me.moMfgGrid.Items(Me.moMfgGrid.EditItemIndex).Cells(Me.GRID_COL_MFG_ALIAS).FindControl(DESCRIPTION_IN_GRID_CONTROL_NAME), TextBox).Text
                    .MfgId = GetSelectedItem(CType(moMfgGrid.Rows(moMfgGrid.EditIndex).Cells(GRID_COL_MFG).FindControl(MANUFACTURER_LIST_IN_GRID_CONTROL_NAME), DropDownList))
                    .CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                End With
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try

        End Sub
        Private Sub ReturnFromEditing()

            moMfgGrid.EditIndex = NO_ROW_SELECTED_INDEX

            If moMfgGrid.PageCount = 0 Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, moMfgGrid, False)
            Else
                ControlMgr.SetVisibleControl(Me, moMfgGrid, True)
            End If
            SetGridControls(moMfgGrid, True)
            State.IsEditMode = False
            PopulateGrid()
            State.PageIndex = moMfgGrid.PageIndex
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
