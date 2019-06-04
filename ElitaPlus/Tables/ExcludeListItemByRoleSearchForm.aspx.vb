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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles Me.PageReturn
            Try
                Me.IsReturningFromChild = True
                Dim retObj As ReturnType = CType(ReturnPar, ReturnType)
                If Not retObj Is Nothing AndAlso retObj.BoChanged Then
                    Me.State.searchDV = Nothing
                End If
                If Not retObj Is Nothing Then
                    Select Case retObj.LastOperation
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Me.State.moExcludeListItemByRoleId = retObj.ExcludeListItemByRoleId
                            Me.State.IsGridVisible = True
                        Case ElitaPlusPage.DetailPageCommand.Delete
                            Me.AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                            Me.State.moExcludeListItemByRoleId = Guid.Empty
                        Case Else
                            Me.State.moExcludeListItemByRoleId = Guid.Empty
                    End Select
                    Grid.PageIndex = Me.State.PageIndex
                    cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
                    Grid.PageSize = Me.State.PageSize
                    ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
                    Me.State.CompanyId = retObj.CompanyIdSearchParam
                    Me.State.ListId = retObj.ListIdSearchParam
                    Me.State.ListItemId = retObj.ListItemIdSearchParam
                    Me.State.RoleId = retObj.RoleIdSearchParam

                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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

            Public Sub New(ByVal LastOp As ElitaPlusPage.DetailPageCommand, ByVal oExcludeListItemByRoleId As Guid, ByVal SearchParam As ExcludeListItemByRoleForm.Parameters, Optional ByVal boChanged As Boolean = False)
                Me.LastOperation = LastOp
                Me.ExcludeListItemByRoleId = oExcludeListItemByRoleId
                Me.BoChanged = boChanged
                If Not SearchParam Is Nothing Then
                    Me.CompanyIdSearchParam = SearchParam.CompanyId
                    Me.ListIdSearchParam = SearchParam.ListId
                    Me.ListItemIdSearchParam = SearchParam.ListItemId
                    Me.RoleIdSearchParam = SearchParam.RoleId
                End If
            End Sub

        End Class

        '#End Region
#Region "Page_Events"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                Me.MasterPage.MessageController.Clear_Hide()
                'SetSession()
                'grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                If Not Page.IsPostBack Then

                    Me.MasterPage.MessageController.Clear()
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                    UpdateBreadCrum()

                    TranslateGridHeader(Grid)

                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Me.SortDirection = Me.State.SortExpression
                    cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
                    PopulateDropdown()
                    If Me.State.IsGridVisible Then
                        If Not (Me.State.PageSize = DEFAULT_NEW_UI_PAGE_SIZE) Or Not (State.PageSize = Grid.PageSize) Then
                            Grid.PageSize = Me.State.PageSize
                        End If
                        Me.PopulateGrid()
                    End If
                    Me.SetGridItemStyleColor(Me.Grid)
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
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
                CompanyMultipleDrop.SelectedGuid = Me.State.CompanyId

            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(EXCLUDE_LIST_ITEM_BY_ROLE_LIST_FORM)
                Me.MasterPage.MessageController.AddError(ex.Message, False)
                Me.MasterPage.MessageController.Show()
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
                BindSelectItem(Me.State.ListId.ToString, moListDescriptionDrop) 'ListItemsExcludeByRole
                If Not Me.State.ListId.Equals(Guid.Empty) Then
                    Dim strListCode As String = LookupListNew.GetCodeFromId(LookupListCache.LK_LIST, Me.GetSelectedItem(moListDescriptionDrop))

                    'Me.BindListControlToDataView(moListItemDescriptionDrop, LookupListNew.DropdownLookupList(strListCode, oLanguageId))
                    Dim listeItemLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(strListCode, Thread.CurrentPrincipal.GetLanguageCode())
                    moListItemDescriptionDrop.Populate(listeItemLkl, New PopulateOptions() With
                                  {
                                  .AddBlankItem = True,
                                  .ValueFunc = AddressOf .GetCode
                                  })
                    BindSelectItem(Me.State.ListItemId.ToString, moListItemDescriptionDrop)
                End If
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(EXCLUDE_LIST_ITEM_BY_ROLE_LIST_FORM)
                Me.MasterPage.MessageController.AddError(ex.Message, False)
                Me.MasterPage.MessageController.Show()
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
                BindSelectItem(Me.State.RoleId.ToString, moRoleDescriptionDrop)
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(EXCLUDE_LIST_ITEM_BY_ROLE_LIST_FORM)
                Me.MasterPage.MessageController.AddError(ex.Message, False)
                Me.MasterPage.MessageController.Show()
            End Try
        End Sub

        Private Sub BindDataGrid(ByVal oDataView As DataView)
            Grid.DataSource = oDataView
            Grid.DataBind()
        End Sub


        Private Sub PopulateGrid(Optional ByVal oAction As String = POPULATE_ACTION_NONE)
            Dim oDataView As DataView

            Try
                If (Me.State.searchDV Is Nothing) Then
                    Me.State.searchDV = ExcludeListitemByRole.getList(CompanyMultipleDrop.SelectedGuid, Me.GetSelectedItem(moListDescriptionDrop), Me.GetSelectedItem(moListItemDescriptionDrop), Me.GetSelectedItem(moRoleDescriptionDrop), ElitaPlusIdentity.Current.ActiveUser.LanguageId)

                End If

                If (Me.State.searchDV.Count = 0) Then

                    Me.State.bnoRow = True
                    CreateHeaderForEmptyGrid(Grid, Me.SortDirection)
                Else
                    Me.State.bnoRow = False
                    Me.Grid.Enabled = True
                End If

                Me.State.searchDV.Sort = Me.State.SortExpression
                Grid.AutoGenerateColumns = False

                Grid.Columns(Me.GRID_COL_COMPANY).SortExpression = ExcludeListitemByRole.ExcludeListitemByRoleSearchDV.COL_COMPANY_NAME
                Grid.Columns(Me.GRID_COL_LIST_ITEM_DESCRIPTION).SortExpression = ExcludeListitemByRole.ExcludeListitemByRoleSearchDV.COL_LIST_ITEM_DESCRIPTION
                Grid.Columns(Me.GRID_COL_LIST_DESCRIPTION).SortExpression = ExcludeListitemByRole.ExcludeListitemByRoleSearchDV.COL_LIST_DESCRIPTION
                Grid.Columns(Me.GRID_COL_ROLE_DESCRIPTION).SortExpression = ExcludeListitemByRole.ExcludeListitemByRoleSearchDV.COL_ROLE_DESCRIPTION
                HighLightSortColumn(Grid, Me.State.SortExpression)                

                SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.CompanyId, Me.Grid, Me.State.PageIndex)

                Me.Grid.DataSource = Me.State.searchDV
                HighLightSortColumn(Grid, Me.SortDirection)
                Me.Grid.DataBind()

                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

                Session("recCount") = Me.State.searchDV.Count

                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ClearSearch()
            CompanyMultipleDrop.SelectedIndex = 0
            moListItemDescriptionDrop.SelectedIndex = 0
            moListDescriptionDrop.SelectedIndex = 0
            moRoleDescriptionDrop.SelectedIndex = 0
            Me.State.moExcludeListItemByRoleId = Guid.Empty
        End Sub

        Private Function GetDataView() As DataView

            Return (ExcludeListitemByRole.getList(CompanyMultipleDrop.SelectedGuid, Me.GetSelectedItem(moListDescriptionDrop), Me.GetSelectedItem(moListItemDescriptionDrop), Me.GetSelectedItem(moRoleDescriptionDrop), ElitaPlusIdentity.Current.ActiveUser.LanguageId))

        End Function

        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                If (Not Me.State Is Nothing) Then
                    Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("EXCLUDE_LIST_ITEM_BY_ROLE")
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("EXCLUDE_LIST_ITEM_BY_ROLE")
                End If
            End If
        End Sub

#End Region

#Region "Datagrid Related"

        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(ByVal value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.State.PageIndex = Grid.PageSize
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moListDescriptionDropChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles moListDescriptionDrop.SelectedIndexChanged
            Try
                Me.State.ListId = Me.GetSelectedItem(moListDescriptionDrop)
                Dim strListCode As String = LookupListNew.GetCodeFromId(LookupListCache.LK_LIST, Me.State.ListId)

                ' Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                ' Me.BindListControlToDataView(moListItemDescriptionDrop, LookupListNew.DropdownLookupList(strListCode, oLanguageId))
                Dim listeItemLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(strListCode, Thread.CurrentPrincipal.GetLanguageCode())
                moListItemDescriptionDrop.Populate(listeItemLkl, New PopulateOptions() With
                                  {
                                  .AddBlankItem = True,
                                  .ValueFunc = AddressOf .GetCode
                                  })
                BindSelectItem(Me.State.ListItemId.ToString, moListItemDescriptionDrop)
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(EXCLUDE_LIST_ITEM_BY_ROLE_LIST_FORM)
                Me.MasterPage.MessageController.AddError(ex.Message, False)
                Me.MasterPage.MessageController.Show()
            End Try


        End Sub

        Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Me.State.PageIndex = e.NewPageIndex
                Me.State.CompanyId = Guid.Empty
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
            BaseItemCreated(sender, e)
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
                Me.State.SortExpression = Me.SortDirection
                Me.State.PageIndex = 0
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim btnEditItem As LinkButton
                If Not dvRow Is Nothing And Not Me.State.bnoRow Then
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        btnEditItem = CType(e.Row.Cells(Me.GRID_COL_EDIT_IDX).FindControl("SelectAction"), LinkButton)
                        btnEditItem.Text = dvRow(ExcludeListitemByRole.ExcludeListitemByRoleSearchDV.COL_LIST_ITEM_DESCRIPTION).ToString
                        e.Row.Cells(Me.GRID_COL_LIST_DESCRIPTION).Text = dvRow(ExcludeListitemByRole.ExcludeListitemByRoleSearchDV.COL_LIST_DESCRIPTION).ToString
                        e.Row.Cells(Me.GRID_COL_COMPANY).Text = dvRow(ExcludeListitemByRole.ExcludeListitemByRoleSearchDV.COL_COMPANY_NAME).ToString
                        e.Row.Cells(Me.GRID_COL_ROLE_DESCRIPTION).Text = dvRow(ExcludeListitemByRole.ExcludeListitemByRoleSearchDV.COL_ROLE_DESCRIPTION).ToString
                        e.Row.Cells(Me.GRID_COL_EXCLUDE_LIST_ITEM_BY_ROLE_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(ExcludeListitemByRole.ExcludeListitemByRoleSearchDV.COL_EXCLUDE_LIST_ITEM_BY_ROLE_ID), Byte()))
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Try
                If e.CommandName = "SelectAction" Then
                    Dim index As Integer = CInt(e.CommandArgument)
                    Me.State.moExcludeListItemByRoleId = New Guid(Me.Grid.Rows(index).Cells(Me.GRID_COL_EXCLUDE_LIST_ITEM_BY_ROLE_ID_IDX).Text)
                    Me.callPage(ExcludeListItemByRoleForm.URL, New ExcludeListItemByRoleForm.Parameters(Me.State.moExcludeListItemByRoleId, Me.State.CompanyId, Me.State.ListId, Me.State.ListItemId, Me.State.RoleId))

                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

#End Region

#Region " Button Clicks "
        Private Sub moBtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnSearch.Click
            Try
                ' Dim oState As TheState
                If Not State.IsGridVisible Then
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    If Not (Grid.PageSize = DEFAULT_NEW_UI_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(State.PageSize, String)
                        Grid.PageSize = State.PageSize
                    End If
                    Me.State.IsGridVisible = True
                End If
                Grid.PageIndex = Me.NO_PAGE_INDEX
                Grid.DataMember = Nothing
                Me.State.searchDV = Nothing
                SetSession()
                Grid.PageIndex = Me.NO_PAGE_INDEX
                Grid.DataMember = Nothing
                Me.State.searchDV = Nothing
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moBtnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnClear.Click
            Try
                ClearSearch()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                Me.State.moExcludeListItemByRoleId = Guid.Empty
                SetSession()

                Me.callPage(ExcludeListItemByRoleForm.URL, Me.State.moExcludeListItemByRoleId)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "State-Management"

        Private Sub SetSession()
            With Me.State                
                .CompanyId = CompanyMultipleDrop.SelectedGuid
                .ListId = Me.GetSelectedItem(moListDescriptionDrop)
                .ListItemId = Me.GetSelectedItem(moListItemDescriptionDrop)
                .RoleId = Me.GetSelectedItem(moRoleDescriptionDrop)
                .PageIndex = Grid.PageIndex
                .PageSize = Grid.PageSize
                .PageSort = Me.State.SortExpression
                .SearchDataView = Me.State.searchDV
            End With
        End Sub


#End Region
    End Class
End Namespace
