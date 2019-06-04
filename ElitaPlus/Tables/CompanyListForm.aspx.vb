Option Strict On
Option Explicit On 

Partial Class CompanyListForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

    Protected WithEvents ErrorCtrl As ErrorController

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
    Public Const GRID_COL_EDIT_IDX As Integer = 0
    Public Const GRID_COL_DESCRIPTION_IDX As Integer = 0
    Public Const GRID_COL_CODE_IDX As Integer = 1
    Public Const GRID_COL_COMPANY_IDX As Integer = 2

    Public Const GRID_TOTAL_COLUMNS As Integer = 3
    Private Const NOTHING_SELECTED As String = "00000000-0000-0000-0000-000000000000"
    Private Const COMPANYLISTFORM As String = "CompanyListForm.aspx"
#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    ' This class keeps the current state for the search page.
    Class MyState
        Public PageIndex As Integer = 0
        Public DescriptionMask As String
        Public CodeMask As String
        Public CompanyId As Guid = Guid.Empty
        Public CountryId As Guid = Guid.Empty
        Public IsGridVisible As Boolean
        Public searchDV As Company.CompanySearchDV = Nothing
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public SortExpression As String = Company.CompanySearchDV.COL_DESCRIPTION
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

#Region "Page_Events"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        '  Me.ErrorCtrl.Clear_Hide() ' REQ-1295
        Me.MasterPage.MessageController.Clear_Hide() ' REQ-1295
        Me.SetStateProperties()

        Try
            If Not Me.IsPostBack Then

                'REQ-1295
                Me.MasterPage.MessageController.Clear()
                Me.MasterPage.UsePageTabTitleInBreadCrum = False
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                UpdateBreadCrum()
                'REQ-1295 : Changes Completed 

                Me.SortDirection = Me.State.SortExpression
                Me.SetDefaultButton(Me.SearchCodeTextBox, btnSearch)
                Me.SetDefaultButton(Me.SearchDescriptionTextBox, btnSearch)
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                If Me.State.IsGridVisible Then
                    If Not (Me.State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(Me.State.selectedPageSize, String)
                        Grid.PageSize = Me.State.selectedPageSize
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

    'REQ-1295
    Private Sub UpdateBreadCrum()
        If (Not Me.State Is Nothing) Then
            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("COMPANY")
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("COMPANY")
            End If
        End If
    End Sub

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Me.MenuEnabled = True
        Me.IsReturningFromChild = True
        Dim retObj As CompanyForm.ReturnType = CType(ReturnPar, CompanyForm.ReturnType)
        Me.State.HasDataChanged = retObj.HasDataChanged
        Select Case retObj.LastOperation
            Case ElitaPlusPage.DetailPageCommand.Back
                If Not retObj Is Nothing Then
                    'Me.State.CompanyId = CType(Session(ELPWebConstants.OLDCOMPANYID), Guid)
                    'Me.UpdateUserCompany()
                    'Session(ELPWebConstants.OLDCOMPANYID) = Nothing
                    Me.State.IsGridVisible = True
                End If
            Case ElitaPlusPage.DetailPageCommand.Delete
                Me.AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
        End Select
    End Sub

#End Region

#Region "Controlling Logic"

    Public Sub PopulateGrid()
        If ((Me.State.searchDV Is Nothing) OrElse (Me.State.HasDataChanged)) Then
            Me.State.searchDV = Company.getList(Me.State.DescriptionMask, Me.State.CodeMask)
            'Ticket # 748479 - Search grids in Tables tab should not show pop-up message when number of retrieved record is over 1,000
            'If (Not (Me.State.HasDataChanged)) Then
            '    'Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
            'End If
        End If
        Me.State.searchDV.Sort = Me.SortDirection

        Me.Grid.AutoGenerateColumns = False
        'Me.Grid.Columns(Me.GRID_COL_CODE_IDX).SortExpression = Company.CompanySearchDV.COL_CODE
        'Me.Grid.Columns(Me.GRID_COL_DESCRIPTION_IDX).SortExpression = Company.CompanySearchDV.COL_DESCRIPTION

        SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.CompanyId, Me.Grid, Me.State.PageIndex)
        Me.SortAndBindGrid()
    End Sub

    Private Sub SortAndBindGrid()
        Me.State.PageIndex = Me.Grid.PageIndex
        'Me.Grid.PageIndex = Me.State.PageIndex
        'Me.Grid.DataSource = Me.State.searchDV
        'HighLightSortColumn(Grid, Me.SortDirection)
        'Me.Grid.DataBind()
        If (Me.State.searchDV.Count = 0) Then

            Me.State.bnoRow = True
            CreateHeaderForEmptyGrid(Grid, Me.SortDirection)
        Else
            Me.State.bnoRow = False
            Me.Grid.Enabled = True
            Me.Grid.DataSource = Me.State.searchDV
            HighLightSortColumn(Grid, Me.SortDirection)
            Me.Grid.DataBind()
        End If
        If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

        ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)

        ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

        Session("recCount") = Me.State.searchDV.Count

        If Me.State.searchDV.Count > 0 Then

            If Me.Grid.Visible Then
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        Else
            If Me.Grid.Visible Then
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End If
    End Sub

#End Region

#Region " Datagrid Related "
    Public Property SortDirection() As String
        Get
            Return ViewState("SortDirection").ToString
        End Get
        Set(ByVal value As String)
            ViewState("SortDirection") = value
        End Set
    End Property
    'The Binding Logic is here
    Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
        Dim btnEditItem As LinkButton
        If Not dvRow Is Nothing And Not Me.State.bnoRow Then
            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                btnEditItem = CType(e.Row.Cells(Me.GRID_COL_DESCRIPTION_IDX).FindControl("SelectAction"), LinkButton)
                btnEditItem.Text = dvRow(Company.CompanySearchDV.COL_DESCRIPTION).ToString
                e.Row.Cells(Me.GRID_COL_CODE_IDX).Text = dvRow(Company.CompanySearchDV.COL_CODE).ToString
                ' e.Row.Cells(Me.GRID_COL_DESCRIPTION_IDX).Text = dvRow(Company.CompanySearchDV.COL_DESCRIPTION).ToString
                e.Row.Cells(Me.GRID_COL_COMPANY_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(Company.CompanySearchDV.COL_COMPANY_ID), Byte()))
            End If
        End If
    End Sub

    Public Sub ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            If e.CommandName = "SelectAction" Then
                Dim index As Integer = CInt(e.CommandArgument)
                Me.State.CompanyId = New Guid(Me.Grid.Rows(index).Cells(Me.GRID_COL_COMPANY_IDX).Text.ToString())
                Me.callPage(CompanyForm.URL, Me.State.CompanyId)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
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

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging

        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.State.CompanyId = Guid.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region " Button Clicks "
    Private Sub SetStateProperties()

        Me.State.DescriptionMask = SearchDescriptionTextBox.Text
        Me.State.CodeMask = SearchCodeTextBox.Text
        'Me.State.CompanyId = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.State.PageIndex = 0
            Me.State.CompanyId = Guid.Empty
            Me.State.IsGridVisible = True
            Me.State.searchDV = Nothing
            Me.State.HasDataChanged = False
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnAdd_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd_WRITE.Click
        'UpdateUserCompany()
        Me.callPage(CompanyForm.URL)
    End Sub

    Private Sub btnClearSearch_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        ClearSearchCriteria()
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
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

#End Region

#Region "Error Handling"


#End Region

End Class
