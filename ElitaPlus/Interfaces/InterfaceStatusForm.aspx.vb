Option Strict On
Option Explicit On

Partial Class InterfaceStatusForm
    Inherits ElitaPlusSearchPage
    Protected WithEvents ErrorCtrl As ErrorController

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    ' NOTE: The following placeholder declaration is required by the Web Form Designer.
    ' Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        ' Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants"
    Public Const GRID_COL_RESET_IDX As Integer = 0
    Public Const GRID_COL_ACTIVE_FILE_IDX As Integer = 1
    Public Const GRID_COL_DESCRIPTION_IDX As Integer = 2
    Public Const GRID_COL_STATUS_IDX As Integer = 3
    Public Const GRID_COL_CREATED_DATE_IDX As Integer = 4
    Public Const GRID_COL_INTERFACE_STATUS_IDX As Integer = 5

    Public Const GRID_TOTAL_COLUMNS As Integer = 6

    Private Const STATUS_PROPERTY As String = "Status"
    Private Const ACTIVE_FILENAME_PROPERTY As String = "Active_Filename"

    Private Const MaxHourvalue As Integer = 12

#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    ' This class keeps the current state for the search page.
    Class MyState
        Public PageIndex As Integer = 0
        Public Description As String
        Public Status As String
        Public activefilename As String
        Public InterfacestatusId As Guid
        Public IsGridVisible As Boolean
        Public searchDV As InterfaceStatusWrk.InterfaceStatusSearchDV = Nothing
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public SortExpression As String = InterfaceStatusWrk.InterfaceStatusSearchDV.COL_DESCRIPTION
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

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ErrorCtrl.Clear_Hide()
        SetStateProperties()
        Try
            If Not IsPostBack Then
                SortDirection = State.SortExpression
                SetDefaultButton(SearchActiveFileTextBox, btnSearch)
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
        MenuEnabled = True
        IsReturningFromChild = True
        Dim retObj As CountryForm.ReturnType = CType(ReturnPar, CountryForm.ReturnType)
        State.HasDataChanged = retObj.HasDataChanged
        Select Case retObj.LastOperation
            Case ElitaPlusPage.DetailPageCommand.Back
                If retObj IsNot Nothing Then
                    If Not retObj.EditingBo.IsNew Then
                        State.InterfacestatusId = retObj.EditingBo.Id
                    End If
                    State.IsGridVisible = True
                End If

        End Select
    End Sub

#End Region

#Region "Controlling Logic"

    Public Sub PopulateGrid()
        If ((State.searchDV Is Nothing) OrElse (State.HasDataChanged)) Then
            State.searchDV = InterfaceStatusWrk.getList(State.activefilename)

        End If
        State.searchDV.Sort = State.SortExpression
        If Not (State.searchDV Is Nothing) Then
            State.searchDV.Sort = SortDirection
            Grid.AutoGenerateColumns = False

            SetPageAndSelectedIndexFromGuid(State.searchDV, State.InterfacestatusId, Grid, State.PageIndex)
            SortAndBindGrid()
        End If
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
        If dvRow IsNot Nothing AndAlso Not State.bnoRow Then
            If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                e.Row.Cells(GRID_COL_ACTIVE_FILE_IDX).Text = dvRow(InterfaceStatusWrk.InterfaceStatusSearchDV.COL_ACTIVE_FILE).ToString
                e.Row.Cells(GRID_COL_DESCRIPTION_IDX).Text = dvRow(InterfaceStatusWrk.InterfaceStatusSearchDV.COL_DESCRIPTION).ToString
                e.Row.Cells(GRID_COL_STATUS_IDX).Text = dvRow(InterfaceStatusWrk.InterfaceStatusSearchDV.COL_STATUS).ToString
                e.Row.Cells(GRID_COL_CREATED_DATE_IDX).Text = dvRow(InterfaceStatusWrk.InterfaceStatusSearchDV.COL_CREATED_DATE).ToString
                e.Row.Cells(GRID_COL_INTERFACE_STATUS_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(InterfaceStatusWrk.InterfaceStatusSearchDV.COL_INTERFACE_STATUS_ID), Byte()))
            End If

            ' Def-3458

            Dim ts As TimeSpan
            ts = DateTime.Now.Subtract(CType(dvRow(InterfaceStatusWrk.InterfaceStatusSearchDV.COL_CREATED_DATE), DateTime))

            If e.Row.Cells(GRID_COL_DESCRIPTION_IDX).Text = "Process" Then
                If ts.TotalHours > MaxHourvalue Then
                    e.Row.Cells(GRID_COL_RESET_IDX).FindControl("chkbxreset").Visible = True
                    e.Row.Enabled = True
                Else
                    e.Row.Cells(GRID_COL_RESET_IDX).FindControl("chkbxreset").Visible = False
                    e.Row.Enabled = False
                End If

            End If
        End If

    End Sub

    Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
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
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub


    Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            State.PageIndex = e.NewPageIndex
            State.InterfacestatusId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub
#End Region

#Region " Button Clicks "
    Private Sub SetStateProperties()

        State.activefilename = SearchActiveFileTextBox.Text()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            State.PageIndex = 0
            State.InterfacestatusId = Guid.Empty
            State.IsGridVisible = True
            State.searchDV = Nothing
            State.HasDataChanged = False
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnClearSearch_Click1(sender As Object, e As System.EventArgs) Handles btnClearSearch.Click
        ClearSearchCriteria()
    End Sub
    Private Sub ClearSearchCriteria()

        Try
            SearchActiveFileTextBox.Text = String.Empty

            'Update Page State
            With State
                .activefilename = SearchActiveFileTextBox.Text

            End With
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub


    Protected Sub chkbxreset_checkedchanged(sender As Object, e As System.EventArgs) 'Handles chkbxreset.CheckedChanged
        Dim row As Integer
        Dim resetchk As CheckBox
        Dim InterfaceStatusWrkId As Guid
        Dim interfaceStatusWrk As InterfaceStatusWrk
        Dim gv As GridViewRow = CType(CType(sender, Control).Parent.Parent, GridViewRow)
        row = gv.RowIndex
        resetchk = CType(Grid.Rows(row).Cells(GRID_COL_RESET_IDX).FindControl("chkbxreset"), CheckBox)
        InterfaceStatusWrkId = New Guid(Grid.Rows(row).Cells(GRID_COL_INTERFACE_STATUS_IDX).Text)
        If resetchk.Checked = True Then
            interfaceStatusWrk = New InterfaceStatusWrk(InterfaceStatusWrkId)
            PopulateBOProperty(interfaceStatusWrk, STATUS_PROPERTY, "Failure")
            PopulateBOProperty(interfaceStatusWrk, ACTIVE_FILENAME_PROPERTY, String.Empty)
            interfaceStatusWrk.Save()
            State.HasDataChanged = True
            PopulateGrid()
            State.HasDataChanged = False

            'Grid.Rows(row).Cells(GRID_COL_ACTIVE_FILE_IDX).Text = " "
            'Grid.Rows(row).Cells(GRID_COL_STATUS_IDX).Text = "Failure"
            'ShowInfoMsgBox("CONTACT BA TO ROLLBACK THE FILE TO PREVIOUS STATUS", True)
        End If

    End Sub


#End Region


#Region "Helpers"

    Private Sub ShowInfoMsgBox(strMsg As String, Optional ByVal Translate As Boolean = True)
        Dim translatedMsg As String = strMsg
        If Translate Then translatedMsg = TranslationBase.TranslateLabelOrMessage(strMsg)
        Dim sJavaScript As String
        sJavaScript = "<SCRIPT>" & Environment.NewLine
        sJavaScript &= "setTimeout(""showMessage('" & translatedMsg & "', '" & "AlertWindow" & "', '" & MSG_BTN_OK & "', '" & MSG_TYPE_INFO & "', '" & "null" & "')"", 0);" & Environment.NewLine
        sJavaScript &= "</SCRIPT>" & Environment.NewLine
        RegisterStartupScript("ShowConfirmation", sJavaScript)
    End Sub
#End Region




#Region "Error Handling"
#End Region

End Class
