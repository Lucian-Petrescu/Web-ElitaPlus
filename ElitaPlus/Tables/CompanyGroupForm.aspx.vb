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
                IsEditing = (Me.Grid.EditItemIndex > NO_ROW_SELECTED_INDEX)
            End Get
        End Property

#End Region
#Region "Page Return"
        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            Try
                Me.IsReturningFromChild = True
                Dim retObj As CompanyGroupDetailForm.ReturnType = CType(ReturnPar, CompanyGroupDetailForm.ReturnType)
                Me.State.HasDataChanged = retObj.BoChanged
                If Not retObj Is Nothing AndAlso retObj.BoChanged Then
                    Me.State.searchDV = Nothing
                End If
                Me.State.IsGridVisible = False
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If Not retObj Is Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                Me.State.SelectedCompanyGroupId = retObj.EditingBo.Id
                                Me.State.IsGridVisible = True
                            End If

                        End If
                    Case ElitaPlusPage.DetailPageCommand.Save
                        If Not retObj Is Nothing Then
                            Me.State.MyBO = New CompanyGroup(retObj.EditingBo.Id)
                            Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete

                        Me.DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                End Select
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

        Private Sub SearchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchButton.Click
            Try

                Me.State.PageIndex = 0
                Me.State.Id = Guid.Empty
                Me.State.searchDV = Nothing
                Me.State.btnsearchclicked = True
                PopulateGrid()
                Me.State.btnsearchclicked = False
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub ClearButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearButton.Click

            ClearSearchCriteria()

        End Sub

        Private Sub ClearSearchCriteria()

            Try
                SearchDescriptionTextBox.Text = String.Empty
                SearchCodeTextBox.Text = String.Empty

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub NewButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click

            Try
                Me.callPage(CompanyGroupDetailForm.url)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

#End Region

#Region "Private Methods"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            'Put user code to initialize the page here
            Try

                ' Populate the header and bredcrumb
                Me.MasterPage.MessageController.Clear()
                Me.MasterPage.UsePageTabTitleInBreadCrum = False
                Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(ADMIN)
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(COMPANY_GROUP)
                Me.UpdateBreadCrum()

                If Not Me.IsPostBack Then
                    Me.SetDefaultButton(Me.SearchCodeTextBox, Me.SearchButton)
                    Me.SetDefaultButton(Me.SearchDescriptionTextBox, Me.SearchButton)

                    PopulateSearchFieldsFromState()



                    If Me.State.IsGridVisible Then
                        If Not (Me.State.selectedPageSize = DEFAULT_NEW_UI_PAGE_SIZE) Then
                            cboPageSize.SelectedValue = CType(Me.State.selectedPageSize, String)
                            Grid.PageSize = Me.State.selectedPageSize
                        End If
                        Me.PopulateGrid()
                    End If
                    Me.SetGridItemStyleColor(Me.Grid)


                    If Me.State.MyBO Is Nothing Then
                        Me.State.MyBO = New CompanyGroup
                        Me.State.IsNew = True
                    End If


                End If
                Me.DisplayNewProgressBarOnClick(Me.SearchButton, LOADING_COMPANY_GROUPS)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)

        End Sub
        Private Sub UpdateBreadCrum()

            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & _
                    TranslationBase.TranslateLabelOrMessage(COMPANY_GROUP)
            End If

        End Sub
        Private Sub PopulateGrid()

            Try
                'Refresh the DataView and Call SetPageAndSelectedIndexFromGuid() to go to the Page 
                'where the most recently saved Record exists in the DataView
                populatestateFromsearchFields()
                If (Me.State.searchDV Is Nothing) Then
                    Me.State.searchDV = CompanyGroup.LoadList(Me.State.DescriptionMask, Me.State.CodeMask, , Me.State.btnsearchclicked)

                End If


                If Me.State.searchDV.Count > 0 Then
                    Me.State.IsGridVisible = True
                Else
                    Me.State.IsGridVisible = False
                End If

                Me.Grid.PageSize = Me.State.selectedPageSize
                'Ticket # 748479 - Search grids in Tables tab should not show pop-up message when number of retrieved record is over 1,000
                'Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
                Me.State.searchDV.Sort = Me.State.SortExpression


                If Not (Me.State.searchDV Is Nothing) Then
                    Me.Grid.AutoGenerateColumns = False
                    Me.Grid.Columns(Me.DESCRIPTION_COL_IDX).SortExpression = CompanyGroup.CompanyGroupDV.COL_DESCRIPTION
                    Me.Grid.Columns(Me.CODE_COL_IDX).SortExpression = CompanyGroup.CompanyGroupDV.COL_CODE
                    Me.Grid.Columns(Me.CLAIM_GROUP_COL).SortExpression = CompanyGroup.CompanyGroupDV.COL_NAME_CLAIM_NUMBERING_DESCRIPTION
                    Me.Grid.Columns(Me.FTP_SITE_COL).SortExpression = CompanyGroup.CompanyGroupDV.COL_NAME_FTP_SITE

                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.Grid, Me.State.PageIndex)
                    SortAndBindGrid()
                    Me.ValidSearchResultCountNew(Me.State.searchDV.Count, True)
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub


        Private Sub populatestateFromsearchFields()

            Me.State.DescriptionMask = SearchDescriptionTextBox.Text
            Me.State.CodeMask = SearchCodeTextBox.Text


        End Sub
        Private Sub PopulateSearchFieldsFromState()
            SearchDescriptionTextBox.Text = Me.State.DescriptionMask
            SearchCodeTextBox.Text = Me.State.CodeMask
        End Sub

        Private Sub SortAndBindGrid()
            Me.State.PageIndex = Me.Grid.CurrentPageIndex
            Me.Grid.DataSource = Me.State.searchDV
            HighLightSortColumn(Grid, Me.State.SortExpression, Me.IsNewUI)
            Me.Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)



            Session("recCount") = Me.State.searchDV.Count
            If Me.State.searchDV.Count > 0 Then
                ControlMgr.SetVisibleControl(Me, SearchResults, True)
                If Me.Grid.Visible Then

                    Me.lblRecordCount.Text = (Me.State.searchDV.Count) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            Else
                ControlMgr.SetVisibleControl(Me, SearchResults, False)
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

            End If

        End Sub
#End Region

#Region " Datagrid Related "

        'The Binding Logic is here
        Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
            Dim btnEditButtonCode As LinkButton

            If (itemType = ListItemType.Item OrElse _
                itemType = ListItemType.AlternatingItem OrElse _
                itemType = ListItemType.SelectedItem) Then

                If (Not e.Item.Cells(Me.CODE_COL_IDX).FindControl(Me.COL_COMPANY_GROUP_CTRL) Is Nothing) Then
                    btnEditButtonCode = CType(e.Item.Cells(Me.CODE_COL_IDX).FindControl(Me.COL_COMPANY_GROUP_CTRL), LinkButton)
                    btnEditButtonCode.Text = dvRow(CompanyGroup.CompanyGroupDV.COL_CODE).ToString
                    btnEditButtonCode.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(CompanyGroup.CompanyGroupDV.COL_COMPANY_GROUP_ID), Byte()))
                    btnEditButtonCode.CommandName = SELECT_ACTION_COMMAND
                End If
                e.Item.Cells(Me.ID_COL_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(CompanyGroup.CompanyGroupDV.COL_COMPANY_GROUP_ID), Byte()))
                e.Item.Cells(Me.DESCRIPTION_COL_IDX).Text = dvRow(CompanyGroup.CompanyGroupDV.COL_DESCRIPTION).ToString
                e.Item.Cells(Me.CLAIM_GROUP_COL).Text = dvRow(CompanyGroup.CompanyGroupDV.COL_NAME_CLAIM_NUMBERING_DESCRIPTION).ToString()
                e.Item.Cells(Me.FTP_SITE_COL).Text = dvRow(CompanyGroup.CompanyGroupDV.COL_NAME_FTP_SITE).ToString()
                e.Item.Cells(Me.INVOICE_GROUP_COL).Text = dvRow(CompanyGroup.CompanyGroupDV.COL_NAME_INVOICE_NUMBERING_DESCRIPTION).ToString()


            End If
        End Sub

        Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged

            Try
                If (Not (Me.State.IsEditMode)) Then
                    Me.State.PageIndex = e.NewPageIndex
                    Me.Grid.CurrentPageIndex = Me.State.PageIndex
                    Me.PopulateGrid()
                    Me.Grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)

            Try
                Dim index As Integer = e.Item.ItemIndex

                If (e.CommandName = Me.SELECT_ACTION_COMMAND) Then
                    'Do the Edit here

                    'Set the IsEditMode flag to TRUE
                    Me.State.IsEditMode = True


                    Me.State.Id = New Guid(Me.Grid.Items(e.Item.ItemIndex).Cells(Me.ID_COL_IDX).Text)
                    Me.State.MyBO = New CompanyGroup(Me.State.Id)

                    Me.callPage(CompanyGroupDetailForm.url, New CompanyGroupDetailForm.Parameters(Me.State.Id))

                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub ItemCreated(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles Grid.SortCommand

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


        Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As DataGrid, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim desc As TextBox = CType(grid.Items(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(desc)
        End Sub

#End Region



    End Class

End Namespace

