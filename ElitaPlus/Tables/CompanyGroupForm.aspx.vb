Option Strict On
Option Explicit On

Namespace Tables

    Partial Class CompanyGroupForm
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
        Private IsReturningFromChild As Boolean = False

#End Region

#Region "Properties"

        Public ReadOnly Property IsEditing() As Boolean
            Get
                IsEditing = (Grid.EditItemIndex > NO_ROW_SELECTED_INDEX)
            End Get
        End Property

#End Region
#Region "Page Return"
        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            Try
                IsReturningFromChild = True
                Dim retObj As CompanyGroupDetailForm.ReturnType = CType(ReturnPar, CompanyGroupDetailForm.ReturnType)
                State.HasDataChanged = retObj.BoChanged
                If retObj IsNot Nothing AndAlso retObj.BoChanged Then
                    State.searchDV = Nothing
                End If
                State.IsGridVisible = False
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If retObj IsNot Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                State.SelectedCompanyGroupId = retObj.EditingBo.Id
                                State.IsGridVisible = True
                            End If

                        End If
                    Case ElitaPlusPage.DetailPageCommand.Save
                        If retObj IsNot Nothing Then
                            State.MyBO = New CompanyGroup(retObj.EditingBo.Id)
                            AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete

                        DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                End Select
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub
#End Region
#Region "Page State"
        Class MyState
            Public PageIndex As Integer = 0
            Public IsNew As Boolean
            Public MyBO As CompanyGroup
            Public DescriptionMask As String
            Public CodeMask As String
            Public CompanyId As Guid
            Public Id As Guid
            Public IsAfterSave As Boolean
            Public IsEditMode As Boolean
            Public IsGridVisible As Boolean
            Public searchDV As DataView = Nothing
            Public SortExpression As String = CompanyGroup.CompanyGroupDV.COL_DESCRIPTION
            Public selectedPageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
            Public AddingNewRow As Boolean
            Public Canceling As Boolean
            Public HasDataChanged As Boolean
            Public btnsearchclicked As Boolean
            Public SelectedCompanyGroupId As Guid = Guid.Empty
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
        'Protected WithEvents TablesLabel As System.Web.UI.WebControls.Label
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label

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

        Private Const ID_COL_IDX As Integer = 5
        Private Const DESCRIPTION_COL_IDX As Integer = 1
        Private Const CODE_COL_IDX As Integer = 0
        Private Const CLAIM_GROUP_COL As Integer = 2
        Private Const INVOICE_GROUP_COL As Integer = 3
        Private Const FTP_SITE_COL As Integer = 4


        Public Const COL_COMPANY_GROUP_CTRL As String = "btnEditCode"
        Public Const SELECT_ACTION_COMMAND As String = "SelectAction"


        Private Const SORT_COMMAND As String = "Sort"

        Private Const NO_ROW_SELECTED_INDEX As Integer = -1

        Public Const ADMIN As String = "Admin"
        Public Const COMPANY_GROUP As String = "COMPANY_GROUP"
        Public Const LOADING_COMPANY_GROUPS As String = "LOADING_COMPANY_GROUPS"

#End Region

#Region "Button Click Handlers"

        Private Sub SearchButton_Click(sender As System.Object, e As System.EventArgs) Handles SearchButton.Click
            Try

                State.PageIndex = 0
                State.Id = Guid.Empty
                State.searchDV = Nothing
                State.btnsearchclicked = True
                PopulateGrid()
                State.btnsearchclicked = False
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub ClearButton_Click(sender As System.Object, e As System.EventArgs) Handles ClearButton.Click

            ClearSearchCriteria()

        End Sub

        Private Sub ClearSearchCriteria()

            Try
                SearchDescriptionTextBox.Text = String.Empty
                SearchCodeTextBox.Text = String.Empty

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub NewButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew.Click

            Try
                callPage(CompanyGroupDetailForm.url)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

#End Region

#Region "Private Methods"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

            'Put user code to initialize the page here
            Try

                ' Populate the header and bredcrumb
                MasterPage.MessageController.Clear()
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(ADMIN)
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(COMPANY_GROUP)
                UpdateBreadCrum()

                If Not IsPostBack Then
                    SetDefaultButton(SearchCodeTextBox, SearchButton)
                    SetDefaultButton(SearchDescriptionTextBox, SearchButton)

                    PopulateSearchFieldsFromState()



                    If State.IsGridVisible Then
                        If Not (State.selectedPageSize = DEFAULT_NEW_UI_PAGE_SIZE) Then
                            cboPageSize.SelectedValue = CType(State.selectedPageSize, String)
                            Grid.PageSize = State.selectedPageSize
                        End If
                        PopulateGrid()
                    End If
                    SetGridItemStyleColor(Grid)


                    If State.MyBO Is Nothing Then
                        State.MyBO = New CompanyGroup
                        State.IsNew = True
                    End If


                End If
                DisplayNewProgressBarOnClick(SearchButton, LOADING_COMPANY_GROUPS)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)

        End Sub
        Private Sub UpdateBreadCrum()

            If (State IsNot Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & _
                    TranslationBase.TranslateLabelOrMessage(COMPANY_GROUP)
            End If

        End Sub
        Private Sub PopulateGrid()

            Try
                'Refresh the DataView and Call SetPageAndSelectedIndexFromGuid() to go to the Page 
                'where the most recently saved Record exists in the DataView
                populatestateFromsearchFields()
                If (State.searchDV Is Nothing) Then
                    State.searchDV = CompanyGroup.LoadList(State.DescriptionMask, State.CodeMask, , State.btnsearchclicked)

                End If


                If State.searchDV.Count > 0 Then
                    State.IsGridVisible = True
                Else
                    State.IsGridVisible = False
                End If

                Grid.PageSize = State.selectedPageSize
                'Ticket # 748479 - Search grids in Tables tab should not show pop-up message when number of retrieved record is over 1,000
                'Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
                State.searchDV.Sort = State.SortExpression


                If Not (State.searchDV Is Nothing) Then
                    Grid.AutoGenerateColumns = False
                    Grid.Columns(DESCRIPTION_COL_IDX).SortExpression = CompanyGroup.CompanyGroupDV.COL_DESCRIPTION
                    Grid.Columns(CODE_COL_IDX).SortExpression = CompanyGroup.CompanyGroupDV.COL_CODE
                    Grid.Columns(CLAIM_GROUP_COL).SortExpression = CompanyGroup.CompanyGroupDV.COL_NAME_CLAIM_NUMBERING_DESCRIPTION
                    Grid.Columns(FTP_SITE_COL).SortExpression = CompanyGroup.CompanyGroupDV.COL_NAME_FTP_SITE

                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.Id, Grid, State.PageIndex)
                    SortAndBindGrid()
                    ValidSearchResultCountNew(State.searchDV.Count, True)
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub


        Private Sub populatestateFromsearchFields()

            State.DescriptionMask = SearchDescriptionTextBox.Text
            State.CodeMask = SearchCodeTextBox.Text


        End Sub
        Private Sub PopulateSearchFieldsFromState()
            SearchDescriptionTextBox.Text = State.DescriptionMask
            SearchCodeTextBox.Text = State.CodeMask
        End Sub

        Private Sub SortAndBindGrid()
            State.PageIndex = Grid.CurrentPageIndex
            Grid.DataSource = State.searchDV
            HighLightSortColumn(Grid, State.SortExpression, IsNewUI)
            Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)



            Session("recCount") = State.searchDV.Count
            If State.searchDV.Count > 0 Then
                ControlMgr.SetVisibleControl(Me, SearchResults, True)
                If Grid.Visible Then

                    lblRecordCount.Text = (State.searchDV.Count) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            Else
                ControlMgr.SetVisibleControl(Me, SearchResults, False)
                lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

            End If

        End Sub
#End Region

#Region " Datagrid Related "

        'The Binding Logic is here
        Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
            Dim btnEditButtonCode As LinkButton

            If (itemType = ListItemType.Item OrElse _
                itemType = ListItemType.AlternatingItem OrElse _
                itemType = ListItemType.SelectedItem) Then

                If (e.Item.Cells(CODE_COL_IDX).FindControl(COL_COMPANY_GROUP_CTRL) IsNot Nothing) Then
                    btnEditButtonCode = CType(e.Item.Cells(CODE_COL_IDX).FindControl(COL_COMPANY_GROUP_CTRL), LinkButton)
                    btnEditButtonCode.Text = dvRow(CompanyGroup.CompanyGroupDV.COL_CODE).ToString
                    btnEditButtonCode.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(CompanyGroup.CompanyGroupDV.COL_COMPANY_GROUP_ID), Byte()))
                    btnEditButtonCode.CommandName = SELECT_ACTION_COMMAND
                End If
                e.Item.Cells(ID_COL_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(CompanyGroup.CompanyGroupDV.COL_COMPANY_GROUP_ID), Byte()))
                e.Item.Cells(DESCRIPTION_COL_IDX).Text = dvRow(CompanyGroup.CompanyGroupDV.COL_DESCRIPTION).ToString
                e.Item.Cells(CLAIM_GROUP_COL).Text = dvRow(CompanyGroup.CompanyGroupDV.COL_NAME_CLAIM_NUMBERING_DESCRIPTION).ToString()
                e.Item.Cells(FTP_SITE_COL).Text = dvRow(CompanyGroup.CompanyGroupDV.COL_NAME_FTP_SITE).ToString()
                e.Item.Cells(INVOICE_GROUP_COL).Text = dvRow(CompanyGroup.CompanyGroupDV.COL_NAME_INVOICE_NUMBERING_DESCRIPTION).ToString()


            End If
        End Sub

        Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged

            Try
                If (Not (State.IsEditMode)) Then
                    State.PageIndex = e.NewPageIndex
                    Grid.CurrentPageIndex = State.PageIndex
                    PopulateGrid()
                    Grid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)

            Try
                Dim index As Integer = e.Item.ItemIndex

                If (e.CommandName = SELECT_ACTION_COMMAND) Then
                    'Do the Edit here

                    'Set the IsEditMode flag to TRUE
                    State.IsEditMode = True


                    State.Id = New Guid(Grid.Items(e.Item.ItemIndex).Cells(ID_COL_IDX).Text)
                    State.MyBO = New CompanyGroup(State.Id)

                    callPage(CompanyGroupDetailForm.url, New CompanyGroupDetailForm.Parameters(State.Id))

                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

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
                HandleErrors(ex, MasterPage.MessageController)
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
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub


        Private Sub SetFocusOnEditableFieldInGrid(grid As DataGrid, cellPosition As Integer, controlName As String, itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim desc As TextBox = CType(grid.Items(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(desc)
        End Sub

#End Region



    End Class

End Namespace

