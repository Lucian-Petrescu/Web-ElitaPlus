Option Strict On
Option Explicit On

Partial Class ProducerSearchForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

    Protected WithEvents ErrorCtrl As ErrorController

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
    Public Const GRID_COL_EDIT_IDX As Integer = 0
    Public Const GRID_COL_DESCRIPTION_IDX As Integer = 0
    Public Const GRID_COL_CODE_IDX As Integer = 1
    Public Const GRID_COL_PRODUCER_TYPE_IDX As Integer = 2
    Public Const GRID_COL_PRODUCER_ID_IDX As Integer = 3

    Public Const GRID_TOTAL_COLUMNS As Integer = 4
    Private Const NOTHING_SELECTED As String = "00000000-0000-0000-0000-000000000000"
    Private Const PRODUCERSEARCHFORM As String = "ProducerSearchForm.aspx"
#End Region

#Region "Page State"

    ' This class keeps the current state for the search page.
    Class MyState
        Public PageIndex As Integer = 0
        Public DescriptionMask As String
        Public CodeMask As String
        Public ProducerId As Guid = Guid.Empty
        Public CountryId As Guid = Guid.Empty
        Public IsGridVisible As Boolean
        Public searchDV As Producer.ProducerSearchDV = Nothing
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public SortExpression As String = Company.CompanySearchDV.COL_DESCRIPTION
        Public HasDataChanged As Boolean
        Public bnoRow As Boolean = False
        Private msPageSort As String
        Sub New()

        End Sub
        Public Property PageSize() As Integer
            Get
                Return selectedPageSize
            End Get
            Set(Value As Integer)
                selectedPageSize = Value
            End Set
        End Property
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
#Region "Page Return Type"
    Private IsReturningFromChild As Boolean = False
    Private Sub ProducerForm_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles Me.PageReturn
        Try
            MenuEnabled = True
            IsReturningFromChild = True
            Dim retObj As ProducerForm.ReturnType = CType(ReturnPar, ProducerForm.ReturnType)
            State.HasDataChanged = retObj.HasDataChanged
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If retObj IsNot Nothing Then
                        State.ProducerId = retObj.EditingBo.Id
                        State.IsGridVisible = True
                    End If
                Case ElitaPlusPage.DetailPageCommand.Delete
                    DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)

            End Select
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region
#Region "Page_Events"

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        '  Me.ErrorCtrl.Clear_Hide() ' REQ-1295
        MasterPage.MessageController.Clear_Hide() ' REQ-1295
        SetStateProperties()

        Try
            If Not IsPostBack Then

                'REQ-1295
                MasterPage.MessageController.Clear()
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                UpdateBreadCrum()
                'REQ-1295 : Changes Completed 

                SortDirection = State.SortExpression
                SetDefaultButton(SearchCodeTextBox, btnSearch)
                SetDefaultButton(SearchDescriptionTextBox, btnSearch)
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                If State.IsGridVisible Then
                    If Not (State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(State.selectedPageSize, String)
                        Grid.PageSize = State.selectedPageSize
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

    'REQ-1295
    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            If (State IsNot Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("PRODUCER")
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("PRODUCER")
            End If
        End If
    End Sub

    'Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
    '    Me.MenuEnabled = True
    '    Me.IsReturningFromChild = True
    '    Dim retObj As ProducerForm.ReturnType = CType(ReturnPar, ProducerForm.ReturnType)
    '    Me.State.HasDataChanged = retObj.HasDataChanged
    '    Select Case retObj.LastOperation
    '        Case ElitaPlusPage.DetailPageCommand.Back
    '            If Not retObj Is Nothing Then
    '                Me.State.IsGridVisible = True
    '            End If
    '        Case ElitaPlusPage.DetailPageCommand.Delete
    '            Me.AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
    '    End Select
    'End Sub

#End Region

#Region "Controlling Logic"

    Public Sub PopulateGrid()
        If ((State.searchDV Is Nothing) OrElse (State.HasDataChanged)) Then
            State.searchDV = Producer.getList(State.DescriptionMask, State.CodeMask)
        End If
        State.searchDV.Sort = SortDirection

        Grid.AutoGenerateColumns = False

        SetPageAndSelectedIndexFromGuid(State.searchDV, State.ProducerId, Grid, State.PageIndex)
        SortAndBindGrid()
    End Sub

    Private Sub SortAndBindGrid()
        State.PageIndex = Grid.PageIndex

        If (State.searchDV.Count = 0) Then

            State.bnoRow = True
            CreateHeaderForEmptyGrid(Grid, SortDirection)
        Else
            State.bnoRow = False
            Grid.Enabled = True
            Grid.DataSource = State.searchDV
            HighLightSortColumn(Grid, SortDirection)
            Grid.DataBind()
        End If
        If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

        ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)

        ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

        Session("recCount") = State.searchDV.Count

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

#End Region

#Region " Datagrid Related "
    Public Property SortDirection() As String
        Get
            Return ViewState("SortDirection").ToString
        End Get
        Set(value As String)
            ViewState("SortDirection") = value
        End Set
    End Property
    'The Binding Logic is here
    Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
        Dim btnEditItem As LinkButton
        If dvRow IsNot Nothing AndAlso Not State.bnoRow Then
            If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                btnEditItem = CType(e.Row.Cells(GRID_COL_DESCRIPTION_IDX).FindControl("SelectAction"), LinkButton)
                btnEditItem.Text = dvRow(Producer.ProducerSearchDV.COL_DESCRIPTION).ToString
                e.Row.Cells(GRID_COL_CODE_IDX).Text = dvRow(Producer.ProducerSearchDV.COL_CODE).ToString
                e.Row.Cells(GRID_COL_PRODUCER_TYPE_IDX).Text = dvRow(Producer.ProducerSearchDV.COL_PRODUCER_TYPE).ToString
                e.Row.Cells(GRID_COL_PRODUCER_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(Producer.ProducerSearchDV.COL_PRODUCER_ID), Byte()))
            End If
        End If
    End Sub

    Public Sub ItemCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            If e.CommandName = "SelectAction" Then
                Dim index As Integer = CInt(e.CommandArgument)
                State.ProducerId = New Guid(Grid.Rows(index).Cells(GRID_COL_PRODUCER_ID_IDX).Text.ToString())
                callPage(ProducerForm.URL, State.ProducerId)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
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
            State.ProducerId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region " Button Clicks "
    Private Sub SetStateProperties()

        State.DescriptionMask = SearchDescriptionTextBox.Text
        State.CodeMask = SearchCodeTextBox.Text

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            State.PageIndex = 0
            State.ProducerId = Guid.Empty
            State.IsGridVisible = True
            State.searchDV = Nothing
            State.HasDataChanged = False
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnAdd_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd_WRITE.Click
        callPage(ProducerForm.URL)
    End Sub

    Private Sub btnClearSearch_Click1(sender As Object, e As System.EventArgs) Handles btnClearSearch.Click
        ClearSearchCriteria()
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
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

#End Region

#Region "Error Handling"


#End Region

End Class
