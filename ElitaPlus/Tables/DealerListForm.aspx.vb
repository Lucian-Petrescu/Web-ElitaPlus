Option Strict On
Option Explicit On
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Tables

    Partial Class DealerListForm
        Inherits ElitaPlusSearchPage
#Region " Web Form Designer Generated Code "

        'Protected WithEvents ErrorCtrl As ErrorController
        'Protected WithEvents Dealer As System.Web.UI.WebControls.Label

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
        Public Const GRID_COL_EDIT_IDX As Integer = 4
        Public Const GRID_COL_COMPANY_CODE_IDX As Integer = 1
        Public Const GRID_COL_DEALER_NANE_IDX As Integer = 2
        Public Const GRID_COL_DEALER_CODE_IDX As Integer = 0
        Public Const GRID_COL_DEALER_GROUP_IDX As Integer = 3
        Public Const GRID_COL_DEALER_IDX As Integer = 4

        Public Const GRID_TOTAL_COLUMNS As Integer = 5
        Private Const NOTHING_SELECTED As String = "00000000-0000-0000-0000-000000000000"
        Private Const DEALERLISTFORM As String = "DealerListForm.aspx"
        Private Const LABEL_SELECT_DEALERCODE As String = "DEALER"

#End Region

#Region "Page State"
        Private IsReturningFromChild As Boolean = False

        ' This class keeps the current state for the search page.
        Class MyState
            Public PageIndex As Integer = 0
            Public DescriptionMask As String
            Public CodeMask As String
            Public DealerId As Guid = Guid.Empty
            Public IsGridVisible As Boolean
            Public searchDV As Dealer.DealerSearchDV = Nothing
            Public selectedPageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
            Public SortExpression As String = Dealer.DealerSearchDV.COL_DEALER_NAME
            Public HasDataChanged As Boolean
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
#Region "Properties"

        'Public ReadOnly Property DealerMultipleDrop() As MultipleColumnDDLabelControl_New
        '    Get
        '        If moDealerMultipleDrop Is Nothing Then
        '            moDealerMultipleDrop = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl_New)
        '        End If
        '        Return moDealerMultipleDrop
        '    End Get
        'End Property

#End Region
#Region "Page_Events"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            MasterPage.MessageController.Clear_Hide()
            'Me.SetStateProperties()

            Try
                If Not IsPostBack Then

                    ' Me.SetDefaultButton(Me.SearchCodeTextBox, btnSearch)
                    ' Me.SetDefaultButton(Me.SearchDescriptionTextBox, btnSearch)
                    ' Set Master Page Header
                    MasterPage.MessageController.Clear()
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                    UpdateBreadCrum()

                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    SortDirection = State.SortExpression
                    PopulateDropdown()
                    If State.IsGridVisible Then
                        If Not (State.selectedPageSize = DEFAULT_NEW_UI_PAGE_SIZE) Or Not (State.selectedPageSize = Grid.PageSize) Then
                            Grid.PageSize = State.selectedPageSize
                        End If
                        cboPageSize.SelectedValue = CType(State.selectedPageSize, String)
                        PopulateGrid()
                    End If
                    SetGridItemStyleColor(Grid)
                    'Me.TranslateGridHeader(Me.Grid)
                    'Me.TranslateGridControls(Me.Grid)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            Try
                MenuEnabled = True
                IsReturningFromChild = True
                Dim retObj As DealerForm.ReturnType = CType(ReturnPar, DealerForm.ReturnType)
                State.HasDataChanged = retObj.HasDataChanged
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If retObj IsNot Nothing Then
                            State.DealerId = retObj.EditingBo.Id

                            'Me.State.CompanyId = CType(Session(ELPWebConstants.OLDCOMPANYID), Guid)
                            'Me.UpdateUserCompany()
                            'Session(ELPWebConstants.OLDCOMPANYID) = Nothing
                            State.IsGridVisible = True
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        'Me.AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                        DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                End Select
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Controlling Logic"

        Public Sub PopulateGrid()
            If ((State.searchDV Is Nothing) OrElse (State.HasDataChanged)) Then
                'Me.State.searchDV = Dealer.getList(Me.DealerMultipleDrop.SelectedGuid, Me.GetSelectedItem(moDealerGroupDrop), ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
                State.searchDV = Dealer.getList(moDealerMultipleDrop.SelectedGuid, GetSelectedItem(moDealerGroupDrop), ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
                'Ticket # 748479 - Search grids in Tables tab should not show pop-up message when number of retrieved record is over 1,000
                'If (Not (Me.State.HasDataChanged)) Then
                ' Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
                'End If
            End If
            If Not (State.searchDV Is Nothing) Then

                State.searchDV.Sort = SortDirection

                Grid.AutoGenerateColumns = False
                'Me.Grid.Columns(Me.GRID_COL_DEALER_CODE_IDX).SortExpression = Dealer.DealerSearchDV.COL_DEALER
                'Me.Grid.Columns(Me.GRID_COL_DEALER_NANE_IDX).SortExpression = Dealer.DealerSearchDV.COL_DEALER_NAME

                SetPageAndSelectedIndexFromGuid(State.searchDV, State.DealerId, Grid, State.PageIndex)
                SortAndBindGrid()
            End If
        End Sub
        Private Sub PopulateDropdown()
            Dim dg As DealerGroup
            'Me.BindListControlToDataView(Me.moDealerGroupDrop, dg.LoadList(Nothing, Nothing), , , True)
            ' Me.BindListControlToDataView(Me.moDealerGroupDrop, LookupListNew.GetDealerGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id), , , True)
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim dealerGroupByCompanyGroupLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("DealerGroupByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            moDealerGroupDrop.Populate(dealerGroupByCompanyGroupLkl, New PopulateOptions() With
                     {
                  .AddBlankItem = True
                    })
            moDealerMultipleDrop.Caption = TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALERCODE)
            moDealerMultipleDrop.NothingSelected = True

            moDealerMultipleDrop.BindData(LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies))
            moDealerMultipleDrop.AutoPostBackDD = True
        End Sub

        Private Sub OnFromDrop_Changed(fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl_New) _
            Handles moDealerMultipleDrop.SelectedDropChanged
            Try
                State.DealerId = moDealerMultipleDrop.SelectedGuid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub SortAndBindGrid()
            If (State.searchDV.Count = 0) Then

                State.bnoRow = True
                CreateHeaderForEmptyGrid(Grid, SortDirection)
            Else
                State.bnoRow = False
                Grid.Enabled = True
                Grid.DataSource = State.searchDV
                HighLightSortColumn(Grid, SortDirection)
                Grid.DataBind()
                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
            End If
            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

            If State.searchDV.Count > 0 Then

                If Grid.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            Else
                If Grid.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
        End Sub

        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                If (State IsNot Nothing) Then
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("DEALER")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("DEALER")
                End If
            End If
        End Sub

#End Region

#Region " GridView Related "
        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

        'The Binding Logic is here
        Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim btnEditItem As LinkButton
                If dvRow IsNot Nothing And Not State.bnoRow Then
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        btnEditItem = CType(e.Row.Cells(GRID_COL_DEALER_CODE_IDX).FindControl("SelectAction"), LinkButton)
                        btnEditItem.Text = dvRow(Dealer.DealerSearchDV.COL_DEALER).ToString
                        e.Row.Cells(GRID_COL_COMPANY_CODE_IDX).Text = dvRow(Dealer.DealerSearchDV.COL_COMPANY).ToString
                        e.Row.Cells(GRID_COL_DEALER_NANE_IDX).Text = dvRow(Dealer.DealerSearchDV.COL_DEALER_NAME).ToString
                        'e.Row.Cells(Me.GRID_COL_DEALER_CODE_IDX).Text = dvRow(Dealer.DealerSearchDV.COL_DEALER).ToString
                        e.Row.Cells(GRID_COL_DEALER_GROUP_IDX).Text = dvRow(Dealer.DealerSearchDV.COL_DEALER_GROUP).ToString
                        e.Row.Cells(GRID_COL_DEALER_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(Dealer.DealerSearchDV.COL_DEALER_ID), Byte()))
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
                    State.DealerId = New Guid(Grid.Rows(index).Cells(GRID_COL_DEALER_IDX).Text)
                    callPage(DealerForm.URL, State.DealerId)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Public Sub RowCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                State.selectedPageSize = Grid.PageSize
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
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


        Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                State.PageIndex = e.NewPageIndex
                State.DealerId = Guid.Empty
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region " Button Clicks "
        'Private Sub SetStateProperties()

        'Me.State.DescriptionMask = SearchDescriptionTextBox.Text
        'Me.State.CodeMask = SearchCodeTextBox.Text
        'Me.State.DealerId = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID

        'End Sub

        Private Sub moBtnSearch_Click(sender As Object, e As System.EventArgs) Handles moBtnSearch.Click
            Try
                State.PageIndex = 0
                State.DealerId = Guid.Empty
                If Not State.IsGridVisible Then
                    cboPageSize.SelectedValue = CType(State.selectedPageSize, String)
                    If Not (Grid.PageSize = DEFAULT_NEW_UI_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(State.selectedPageSize, String)
                        Grid.PageSize = State.selectedPageSize
                    End If
                    State.IsGridVisible = True
                End If
                State.searchDV = Nothing
                State.HasDataChanged = False
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnAdd_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd_WRITE.Click
            'UpdateUserCompany()
            callPage(DealerForm.URL)
        End Sub

        Private Sub moBtnClearSearch_Click(sender As Object, e As System.EventArgs) Handles moBtnClearSearch.Click
            ClearSearchCriteria()
        End Sub
        Private Sub ClearSearchCriteria()

            Try
                moDealerMultipleDrop.SelectedIndex = BLANK_ITEM_SELECTED
                'SearchDescriptionTextBox.Text = String.Empty
                'SearchCodeTextBox.Text = String.Empty
                moDealerGroupDrop.SelectedIndex = 0

                'Update Page State
                'With Me.State
                '    .DescriptionMask = SearchDescriptionTextBox.Text
                '    .CodeMask = SearchCodeTextBox.Text
                'End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

#End Region

    End Class

End Namespace
