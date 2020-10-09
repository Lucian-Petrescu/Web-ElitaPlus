Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Tables

    Partial Class ExcludeListItemByRoleSearchForm
        Inherits ElitaPlusSearchPage

        'Protected WithEvents multipleDropControl As MultipleColumnDDLabelControl

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

        Private Const EXCLUDE_LIST_ITEM_BY_ROLE_LIST_FORM As String = "EXCLUDE_LIST_ITEM_BY_ROLE_LIST_FORM" ' Maintain Exclude List By Role List Exception
        Private Const EXCLUDE_LIST_ITEM_BY_ROLE_DETAIL_PAGE As String = "ExcludeListItemByRoleForm.aspx"
        Private Const EXCLUDE_LIST_ITEM_BY_ROLE_STATE As String = "ExcludeListItemByRoleState"
        Private Const LABEL_COMPANY As String = "COMPANY"

#Region "Constants"
        Public Const GRID_COL_EDIT_IDX As Integer = 0
        Public Const GRID_COL_LIST_ITEM_DESCRIPTION As Integer = 0
        Public Const GRID_COL_COMPANY As Integer = 1
        Public Const GRID_COL_LIST_DESCRIPTION As Integer = 2
        Public Const GRID_COL_ROLE_DESCRIPTION As Integer = 3
        Public Const GRID_COL_EXCLUDE_LIST_ITEM_BY_ROLE_ID_IDX As Integer = 4

        Public Const GRID_TOTAL_COLUMNS As Integer = 5
        Private Const NOTHING_SELECTED As String = "00000000-0000-0000-0000-000000000000"


#End Region
#End Region

#Region "Properties"

        'Public ReadOnly Property CompanyMultipleDrop() As MultipleColumnDDLabelControl
        '    Get
        '        If multipleDropControl Is Nothing Then
        '            multipleDropControl = CType(FindControl("moCompanyMultipleDrop"), MultipleColumnDDLabelControl)
        '        End If
        '        Return multipleDropControl
        '    End Get
        'End Property

#End Region

#Region "Page State"

        ' This class keeps the current state for the search page.
        Class MyState

            Public IsGridVisible As Boolean = False
            Public searchDV As ExcludeListitemByRole.ExcludeListitemByRoleSearchDV = Nothing

            Public SortExpression As String = ExcludeListitemByRole.ExcludeListitemByRoleSearchDV.COL_LIST_ITEM_DESCRIPTION
            Public PageIndex As Integer = 0
            Public PageSort As String
            Public PageSize As Integer = 15
            Public SearchDataView As ExcludeListitemByRole.ExcludeListitemByRoleSearchDV
            Public moExcludeListItemByRoleId As Guid = Guid.Empty

            Public CompanyId As Guid = Guid.Empty
            Public ListId As Guid = Guid.Empty
            Public ListItemId As Guid = Guid.Empty
            Public RoleId As Guid = Guid.Empty
            Public bnoRow As Boolean = False

            Sub New()

            End Sub

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

#Region "Page Return"

        Private IsReturningFromChild As Boolean = False

        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles Me.PageReturn
            Try
                IsReturningFromChild = True
                Dim retObj As ReturnType = CType(ReturnPar, ReturnType)
                If retObj IsNot Nothing AndAlso retObj.BoChanged Then
                    State.searchDV = Nothing
                End If
                If retObj IsNot Nothing Then
                    Select Case retObj.LastOperation
                        Case ElitaPlusPage.DetailPageCommand.Back
                            State.moExcludeListItemByRoleId = retObj.ExcludeListItemByRoleId
                            State.IsGridVisible = True
                        Case ElitaPlusPage.DetailPageCommand.Delete
                            AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                            State.moExcludeListItemByRoleId = Guid.Empty
                        Case Else
                            State.moExcludeListItemByRoleId = Guid.Empty
                    End Select
                    Grid.PageIndex = State.PageIndex
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    Grid.PageSize = State.PageSize
                    ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
                    State.CompanyId = retObj.CompanyIdSearchParam
                    State.ListId = retObj.ListIdSearchParam
                    State.ListItemId = retObj.ListItemIdSearchParam
                    State.RoleId = retObj.RoleIdSearchParam

                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub



#End Region

        Public Class ReturnType
            Public LastOperation As ElitaPlusPage.DetailPageCommand
            Public ExcludeListItemByRoleId As Guid
            Public CompanyIdSearchParam As Guid
            Public ListIdSearchParam As Guid
            Public ListItemIdSearchParam As Guid
            Public RoleIdSearchParam As Guid
            Public BoChanged As Boolean = False

            Public Sub New(LastOp As ElitaPlusPage.DetailPageCommand, oExcludeListItemByRoleId As Guid, SearchParam As ExcludeListItemByRoleForm.Parameters, Optional ByVal boChanged As Boolean = False)
                LastOperation = LastOp
                ExcludeListItemByRoleId = oExcludeListItemByRoleId
                Me.BoChanged = boChanged
                If SearchParam IsNot Nothing Then
                    CompanyIdSearchParam = SearchParam.CompanyId
                    ListIdSearchParam = SearchParam.ListId
                    ListItemIdSearchParam = SearchParam.ListItemId
                    RoleIdSearchParam = SearchParam.RoleId
                End If
            End Sub

        End Class

        '#End Region
#Region "Page_Events"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                MasterPage.MessageController.Clear_Hide()
                'SetSession()
                'grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                If Not Page.IsPostBack Then

                    MasterPage.MessageController.Clear()
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                    UpdateBreadCrum()

                    TranslateGridHeader(Grid)

                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    SortDirection = State.SortExpression
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    PopulateDropdown()
                    If State.IsGridVisible Then
                        If Not (State.PageSize = DEFAULT_NEW_UI_PAGE_SIZE) OrElse Not (State.PageSize = Grid.PageSize) Then
                            Grid.PageSize = State.PageSize
                        End If
                        PopulateGrid()
                    End If
                    SetGridItemStyleColor(Grid)
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

#End Region

#Region "Controlling Logic"

        Private Sub PopulateDropdown()
            PopulateCompany()
            PopulateList()
            PopulateRoles()

        End Sub

        Private Sub PopulateCompany()
            Try

                Dim oDataView As DataView = LookupListNew.GetCompanyLookupList()
                CompanyMultipleDrop.Caption = TranslationBase.TranslateLabelOrMessage(LABEL_COMPANY)
                CompanyMultipleDrop.NothingSelected = True
                CompanyMultipleDrop.BindData(oDataView)
                CompanyMultipleDrop.AutoPostBackDD = False
                CompanyMultipleDrop.NothingSelected = True
                CompanyMultipleDrop.SelectedGuid = State.CompanyId

            Catch ex As Exception
                MasterPage.MessageController.AddError(EXCLUDE_LIST_ITEM_BY_ROLE_LIST_FORM)
                MasterPage.MessageController.AddError(ex.Message, False)
                MasterPage.MessageController.Show()
            End Try
        End Sub

        Private Sub PopulateList()
            Try
                Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                'Me.BindListControlToDataView(moListDescriptionDrop, LookupListNew.GetList(oLanguageId))
                Dim listcontext As ListContext = New ListContext()
                listcontext.LanguageId = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                Dim listLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ListItemsExcludeByRole", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                moListDescriptionDrop.Populate(listLkl, New PopulateOptions() With
                                  {
                                  .AddBlankItem = True
                                  })
                BindSelectItem(State.ListId.ToString, moListDescriptionDrop) 'ListItemsExcludeByRole
                If Not State.ListId.Equals(Guid.Empty) Then
                    Dim strListCode As String = LookupListNew.GetCodeFromId(LookupListCache.LK_LIST, GetSelectedItem(moListDescriptionDrop))

                    'Me.BindListControlToDataView(moListItemDescriptionDrop, LookupListNew.DropdownLookupList(strListCode, oLanguageId))
                    Dim listeItemLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(strListCode, Thread.CurrentPrincipal.GetLanguageCode())
                    moListItemDescriptionDrop.Populate(listeItemLkl, New PopulateOptions() With
                                  {
                                  .AddBlankItem = True,
                                  .ValueFunc = AddressOf .GetCode
                                  })
                    BindSelectItem(State.ListItemId.ToString, moListItemDescriptionDrop)
                End If
            Catch ex As Exception
                MasterPage.MessageController.AddError(EXCLUDE_LIST_ITEM_BY_ROLE_LIST_FORM)
                MasterPage.MessageController.AddError(ex.Message, False)
                MasterPage.MessageController.Show()
            End Try
        End Sub

        Private Sub PopulateRoles()
            Try
                Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                'Me.BindListControlToDataView(moRoleDescriptionDrop, LookupListNew.GetRolesLookupList)
                Dim listLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.GetRoleList, Thread.CurrentPrincipal.GetLanguageCode())
                moRoleDescriptionDrop.Populate(listLkl, New PopulateOptions() With
                                  {
                                  .AddBlankItem = True
                                  })
                BindSelectItem(State.RoleId.ToString, moRoleDescriptionDrop)
            Catch ex As Exception
                MasterPage.MessageController.AddError(EXCLUDE_LIST_ITEM_BY_ROLE_LIST_FORM)
                MasterPage.MessageController.AddError(ex.Message, False)
                MasterPage.MessageController.Show()
            End Try
        End Sub

        Private Sub BindDataGrid(oDataView As DataView)
            Grid.DataSource = oDataView
            Grid.DataBind()
        End Sub


        Private Sub PopulateGrid(Optional ByVal oAction As String = POPULATE_ACTION_NONE)
            Dim oDataView As DataView

            Try
                If (State.searchDV Is Nothing) Then
                    State.searchDV = ExcludeListitemByRole.getList(CompanyMultipleDrop.SelectedGuid, GetSelectedItem(moListDescriptionDrop), GetSelectedItem(moListItemDescriptionDrop), GetSelectedItem(moRoleDescriptionDrop), ElitaPlusIdentity.Current.ActiveUser.LanguageId)

                End If

                If (State.searchDV.Count = 0) Then

                    State.bnoRow = True
                    CreateHeaderForEmptyGrid(Grid, SortDirection)
                Else
                    State.bnoRow = False
                    Grid.Enabled = True
                End If

                State.searchDV.Sort = State.SortExpression
                Grid.AutoGenerateColumns = False

                Grid.Columns(GRID_COL_COMPANY).SortExpression = ExcludeListitemByRole.ExcludeListitemByRoleSearchDV.COL_COMPANY_NAME
                Grid.Columns(GRID_COL_LIST_ITEM_DESCRIPTION).SortExpression = ExcludeListitemByRole.ExcludeListitemByRoleSearchDV.COL_LIST_ITEM_DESCRIPTION
                Grid.Columns(GRID_COL_LIST_DESCRIPTION).SortExpression = ExcludeListitemByRole.ExcludeListitemByRoleSearchDV.COL_LIST_DESCRIPTION
                Grid.Columns(GRID_COL_ROLE_DESCRIPTION).SortExpression = ExcludeListitemByRole.ExcludeListitemByRoleSearchDV.COL_ROLE_DESCRIPTION
                HighLightSortColumn(Grid, State.SortExpression)                

                SetPageAndSelectedIndexFromGuid(State.searchDV, State.CompanyId, Grid, State.PageIndex)

                Grid.DataSource = State.searchDV
                HighLightSortColumn(Grid, SortDirection)
                Grid.DataBind()

                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

                Session("recCount") = State.searchDV.Count

                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

                lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ClearSearch()
            CompanyMultipleDrop.SelectedIndex = 0
            moListItemDescriptionDrop.SelectedIndex = 0
            moListDescriptionDrop.SelectedIndex = 0
            moRoleDescriptionDrop.SelectedIndex = 0
            State.moExcludeListItemByRoleId = Guid.Empty
        End Sub

        Private Function GetDataView() As DataView

            Return (ExcludeListitemByRole.getList(CompanyMultipleDrop.SelectedGuid, GetSelectedItem(moListDescriptionDrop), GetSelectedItem(moListItemDescriptionDrop), GetSelectedItem(moRoleDescriptionDrop), ElitaPlusIdentity.Current.ActiveUser.LanguageId))

        End Function

        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                If (State IsNot Nothing) Then
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("EXCLUDE_LIST_ITEM_BY_ROLE")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("EXCLUDE_LIST_ITEM_BY_ROLE")
                End If
            End If
        End Sub

#End Region

#Region "Datagrid Related"

        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                State.PageIndex = Grid.PageSize
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moListDescriptionDropChanged(source As Object, e As System.EventArgs) Handles moListDescriptionDrop.SelectedIndexChanged
            Try
                State.ListId = GetSelectedItem(moListDescriptionDrop)
                Dim strListCode As String = LookupListNew.GetCodeFromId(LookupListCache.LK_LIST, State.ListId)

                ' Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                ' Me.BindListControlToDataView(moListItemDescriptionDrop, LookupListNew.DropdownLookupList(strListCode, oLanguageId))
                Dim listeItemLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(strListCode, Thread.CurrentPrincipal.GetLanguageCode())
                moListItemDescriptionDrop.Populate(listeItemLkl, New PopulateOptions() With
                                  {
                                  .AddBlankItem = True,
                                  .ValueFunc = AddressOf .GetCode
                                  })
                BindSelectItem(State.ListItemId.ToString, moListItemDescriptionDrop)
            Catch ex As Exception
                MasterPage.MessageController.AddError(EXCLUDE_LIST_ITEM_BY_ROLE_LIST_FORM)
                MasterPage.MessageController.AddError(ex.Message, False)
                MasterPage.MessageController.Show()
            End Try


        End Sub

        Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                State.PageIndex = e.NewPageIndex
                State.CompanyId = Guid.Empty
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub RowCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
            BaseItemCreated(sender, e)
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
                State.SortExpression = SortDirection
                State.PageIndex = 0
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim btnEditItem As LinkButton
                If dvRow IsNot Nothing AndAlso Not State.bnoRow Then
                    If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                        btnEditItem = CType(e.Row.Cells(GRID_COL_EDIT_IDX).FindControl("SelectAction"), LinkButton)
                        btnEditItem.Text = dvRow(ExcludeListitemByRole.ExcludeListitemByRoleSearchDV.COL_LIST_ITEM_DESCRIPTION).ToString
                        e.Row.Cells(GRID_COL_LIST_DESCRIPTION).Text = dvRow(ExcludeListitemByRole.ExcludeListitemByRoleSearchDV.COL_LIST_DESCRIPTION).ToString
                        e.Row.Cells(GRID_COL_COMPANY).Text = dvRow(ExcludeListitemByRole.ExcludeListitemByRoleSearchDV.COL_COMPANY_NAME).ToString
                        e.Row.Cells(GRID_COL_ROLE_DESCRIPTION).Text = dvRow(ExcludeListitemByRole.ExcludeListitemByRoleSearchDV.COL_ROLE_DESCRIPTION).ToString
                        e.Row.Cells(GRID_COL_EXCLUDE_LIST_ITEM_BY_ROLE_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(ExcludeListitemByRole.ExcludeListitemByRoleSearchDV.COL_EXCLUDE_LIST_ITEM_BY_ROLE_ID), Byte()))
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub RowCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Try
                If e.CommandName = "SelectAction" Then
                    Dim index As Integer = CInt(e.CommandArgument)
                    State.moExcludeListItemByRoleId = New Guid(Grid.Rows(index).Cells(GRID_COL_EXCLUDE_LIST_ITEM_BY_ROLE_ID_IDX).Text)
                    callPage(ExcludeListItemByRoleForm.URL, New ExcludeListItemByRoleForm.Parameters(State.moExcludeListItemByRoleId, State.CompanyId, State.ListId, State.ListItemId, State.RoleId))

                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

#End Region

#Region " Button Clicks "
        Private Sub moBtnSearch_Click(sender As System.Object, e As System.EventArgs) Handles moBtnSearch.Click
            Try
                ' Dim oState As TheState
                If Not State.IsGridVisible Then
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    If Not (Grid.PageSize = DEFAULT_NEW_UI_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(State.PageSize, String)
                        Grid.PageSize = State.PageSize
                    End If
                    State.IsGridVisible = True
                End If
                Grid.PageIndex = NO_PAGE_INDEX
                Grid.DataMember = Nothing
                State.searchDV = Nothing
                SetSession()
                Grid.PageIndex = NO_PAGE_INDEX
                Grid.DataMember = Nothing
                State.searchDV = Nothing
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moBtnClear_Click(sender As System.Object, e As System.EventArgs) Handles moBtnClear.Click
            Try
                ClearSearch()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                State.moExcludeListItemByRoleId = Guid.Empty
                SetSession()

                callPage(ExcludeListItemByRoleForm.URL, State.moExcludeListItemByRoleId)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "State-Management"

        Private Sub SetSession()
            With State                
                .CompanyId = CompanyMultipleDrop.SelectedGuid
                .ListId = GetSelectedItem(moListDescriptionDrop)
                .ListItemId = GetSelectedItem(moListItemDescriptionDrop)
                .RoleId = GetSelectedItem(moRoleDescriptionDrop)
                .PageIndex = Grid.PageIndex
                .PageSize = Grid.PageSize
                .PageSort = State.SortExpression
                .SearchDataView = State.searchDV
            End With
        End Sub


#End Region
    End Class
End Namespace
