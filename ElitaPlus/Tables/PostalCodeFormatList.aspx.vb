Partial Class PostalCodeFormatList
    Inherits ElitaPlusSearchPage

    Protected WithEvents ErrorCtrl As ErrorController

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
    Public Const GRID_COL_EDIT_IDX As Integer = 0
    Public Const GRID_COL_DESCRIPTION_IDX As Integer = 1
    Public Const GRID_COL_SAMPLEFORMAT_IDX As Integer = 2
    Public Const GRID_COL_POSTALCODEFORMAT_IDX As Integer = 3

    Public Const GRID_TOTAL_COLUMNS As Integer = 4


#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    ' This class keeps the current state for the search page.
    Class MyState
        Public PageIndex As Integer = 0
        Public DescriptionMask As String
        Public PostalCodeFormatId As Guid = Guid.Empty
        Public IsGridVisible As Boolean
        Public searchDV As PostalCodeFormat.PostalCodeFormatDV = Nothing
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public SortExpression As String = PostalCodeFormat.PostalCodeFormatDV.COL_DESCRIPTION
        Public HasDataChanged As Boolean

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

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            ErrorCtrl.Clear_Hide()
            SetStateProperties()
            If Not IsPostBack Then
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
            HandleErrors(ex, ErrorCtrl)
        End Try
        ShowMissingTranslations(ErrorCtrl)
    End Sub

    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Try
            MenuEnabled = True
            IsReturningFromChild = True

            Dim retObj As PostalCodeFormatForm.ReturnType = CType(ReturnPar, PostalCodeFormatForm.ReturnType)
            State.HasDataChanged = retObj.HasDataChanged
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If retObj IsNot Nothing Then
                        If Not retObj.EditingBo.IsNew Then
                            State.PostalCodeFormatId = retObj.EditingBo.Id
                        End If
                        State.IsGridVisible = True
                    End If
                Case ElitaPlusPage.DetailPageCommand.Delete
                    AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
            End Select
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub


#End Region

#Region "Controlling Logic"

    Public Sub PopulateGrid()

        If ((State.searchDV Is Nothing) OrElse (State.HasDataChanged)) Then
            State.searchDV = PostalCodeFormat.LoadList(State.DescriptionMask)
            'Ticket # 748479 - Search grids in Tables tab should not show pop-up message when number of retrieved record is over 1,000
            'If (Not (Me.State.HasDataChanged)) Then
            'Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
            'End If
        End If
        State.searchDV.Sort = State.SortExpression
        Grid.AutoGenerateColumns = False
        Grid.Columns(GRID_COL_DESCRIPTION_IDX).SortExpression = PostalCodeFormat.PostalCodeFormatDV.COL_DESCRIPTION
        Grid.Columns(GRID_COL_SAMPLEFORMAT_IDX).SortExpression = PostalCodeFormat.PostalCodeFormatDV.COL_FORMAT
        Grid.Columns(GRID_COL_POSTALCODEFORMAT_IDX).SortExpression = Country.CountrySearchDV.COL_CODE

        SetPageAndSelectedIndexFromGuid(State.searchDV, State.PostalCodeFormatId, Grid, State.PageIndex)
        SortAndBindGrid()
    End Sub

    Private Sub SortAndBindGrid()
        State.PageIndex = Grid.CurrentPageIndex
        Grid.DataSource = State.searchDV
        HighLightSortColumn(Grid, State.SortExpression)
        Grid.DataBind()

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
    Private Function GetSampleFormat(regExString As String) As String
        Try
            If regExString.Trim().Length > 2 Then ' to account for ^ and $
                regExString = regExString.Substring(1, regExString.Trim().Length - 2)
            End If

            Dim previewStr As String = ""
            If regExString.Trim().Length > 0 Then
                For Each tempStr As String In regExString.Trim().Split("}".Chars(0))
                    If tempStr.Trim().Length > 0 Then
                        tempStr = tempStr + "}"
                        Dim tempRegEx As GenericRegExFactory = New GenericRegExFactory(tempStr)
                        Select Case tempRegEx.RegularExp.RegularExType
                            Case RegExTypeValues.SpaceRegEx
                                previewStr = previewStr + New String(" ".Chars(0), tempRegEx.RegularExp.MaximumLength)
                            Case RegExTypeValues.SpecialCharRegEx
                                previewStr = previewStr + New String(CType(tempRegEx.RegularExp, SpecialCharRegEx).SpecialChar.Chars(0), tempRegEx.RegularExp.MaximumLength)
                            Case RegExTypeValues.NumericRegEx
                                previewStr = previewStr + New String("N".Chars(0), tempRegEx.RegularExp.MaximumLength)
                            Case RegExTypeValues.AlphaNumericRegEx
                                previewStr = previewStr + New String("X".Chars(0), tempRegEx.RegularExp.MaximumLength)
                            Case RegExTypeValues.AlphaRegEx
                                previewStr = previewStr + New String("A".Chars(0), tempRegEx.RegularExp.MaximumLength)
                        End Select
                    End If
                Next
            End If
            If previewStr.Trim = "" Then
                previewStr = "This special format has varying character length."
            End If
            Return previewStr
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function


    'The Binding Logic is here
    Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
        If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
            e.Item.Cells(GRID_COL_SAMPLEFORMAT_IDX).Text = GetSampleFormat(dvRow(PostalCodeFormat.PostalCodeFormatDV.COL_FORMAT).ToString)
            e.Item.Cells(GRID_COL_DESCRIPTION_IDX).Text = dvRow(PostalCodeFormat.PostalCodeFormatDV.COL_DESCRIPTION).ToString
            e.Item.Cells(GRID_COL_POSTALCODEFORMAT_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(PostalCodeFormat.PostalCodeFormatDV.COL_POSTALCODE_FORMAT_ID), Byte()))
        End If
    End Sub

    Public Sub ItemCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Try
            If e.CommandName = "SelectAction" Then
                State.PostalCodeFormatId = New Guid(e.Item.Cells(GRID_COL_POSTALCODEFORMAT_IDX).Text)
                callPage(PostalCodeFormatForm.URL, State.PostalCodeFormatId)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

    Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_SortCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles Grid.SortCommand
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
            State.PageIndex = 0
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = e.NewPageIndex
            State.PostalCodeFormatId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub
#End Region

#Region " Button Clicks "
    Private Sub SetStateProperties()

        State.DescriptionMask = SearchDescriptionTextBox.Text

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            State.PageIndex = 0
            State.PostalCodeFormatId = Guid.Empty
            State.IsGridVisible = True
            State.searchDV = Nothing
            State.HasDataChanged = False
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnAdd_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd_WRITE.Click
        callPage(PostalCodeFormatForm.URL)
    End Sub

    Private Sub btnClearSearch_Click1(sender As Object, e As System.EventArgs) Handles btnClearSearch.Click
        ClearSearchCriteria()
    End Sub
    Private Sub ClearSearchCriteria()

        Try
            SearchDescriptionTextBox.Text = String.Empty

            'Update Page State
            With State
                .DescriptionMask = SearchDescriptionTextBox.Text
            End With
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

#End Region

#Region "Error Handling"


#End Region


End Class
