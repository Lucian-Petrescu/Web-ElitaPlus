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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Me.ErrorCtrl.Clear_Hide()
        Me.SetStateProperties()
        Try
            If Not Me.IsPostBack Then
                Me.SortDirection = Me.State.SortExpression
                Me.SetDefaultButton(Me.SearchActiveFileTextBox, btnSearch)
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
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
        Me.ShowMissingTranslations(Me.ErrorCtrl)
    End Sub

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Me.MenuEnabled = True
        Me.IsReturningFromChild = True
        Dim retObj As CountryForm.ReturnType = CType(ReturnPar, CountryForm.ReturnType)
        Me.State.HasDataChanged = retObj.HasDataChanged
        Select Case retObj.LastOperation
            Case ElitaPlusPage.DetailPageCommand.Back
                If Not retObj Is Nothing Then
                    If Not retObj.EditingBo.IsNew Then
                        Me.State.InterfacestatusId = retObj.EditingBo.Id
                    End If
                    Me.State.IsGridVisible = True
                End If

        End Select
    End Sub

#End Region

#Region "Controlling Logic"

    Public Sub PopulateGrid()
        If ((Me.State.searchDV Is Nothing) OrElse (Me.State.HasDataChanged)) Then
            Me.State.searchDV = InterfaceStatusWrk.getList(Me.State.activefilename)

        End If
        Me.State.searchDV.Sort = Me.State.SortExpression
        If Not (Me.State.searchDV Is Nothing) Then
            Me.State.searchDV.Sort = Me.SortDirection
            Me.Grid.AutoGenerateColumns = False

            SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.InterfacestatusId, Me.Grid, Me.State.PageIndex)
            Me.SortAndBindGrid()
        End If
    End Sub

    Private Sub SortAndBindGrid()
        Me.State.PageIndex = Me.Grid.PageIndex

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
        If Not dvRow Is Nothing And Not Me.State.bnoRow Then
            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                e.Row.Cells(Me.GRID_COL_ACTIVE_FILE_IDX).Text = dvRow(InterfaceStatusWrk.InterfaceStatusSearchDV.COL_ACTIVE_FILE).ToString
                e.Row.Cells(Me.GRID_COL_DESCRIPTION_IDX).Text = dvRow(InterfaceStatusWrk.InterfaceStatusSearchDV.COL_DESCRIPTION).ToString
                e.Row.Cells(Me.GRID_COL_STATUS_IDX).Text = dvRow(InterfaceStatusWrk.InterfaceStatusSearchDV.COL_STATUS).ToString
                e.Row.Cells(Me.GRID_COL_CREATED_DATE_IDX).Text = dvRow(InterfaceStatusWrk.InterfaceStatusSearchDV.COL_CREATED_DATE).ToString
                e.Row.Cells(Me.GRID_COL_INTERFACE_STATUS_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(InterfaceStatusWrk.InterfaceStatusSearchDV.COL_INTERFACE_STATUS_ID), Byte()))
            End If

            ' Def-3458

            Dim ts As TimeSpan
            ts = DateTime.Now.Subtract(CType(dvRow(InterfaceStatusWrk.InterfaceStatusSearchDV.COL_CREATED_DATE), DateTime))

            If e.Row.Cells(Me.GRID_COL_DESCRIPTION_IDX).Text = "Process" Then
                If ts.TotalHours > MaxHourvalue Then
                    e.Row.Cells(Me.GRID_COL_RESET_IDX).FindControl("chkbxreset").Visible = True
                    e.Row.Enabled = True
                Else
                    e.Row.Cells(Me.GRID_COL_RESET_IDX).FindControl("chkbxreset").Visible = False
                    e.Row.Enabled = False
                End If

            End If
        End If

    End Sub

    Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
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
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub


    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.State.InterfacestatusId = Guid.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub
#End Region

#Region " Button Clicks "
    Private Sub SetStateProperties()

        Me.State.activefilename = SearchActiveFileTextBox.Text()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.State.PageIndex = 0
            Me.State.InterfacestatusId = Guid.Empty
            Me.State.IsGridVisible = True
            Me.State.searchDV = Nothing
            Me.State.HasDataChanged = False
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnClearSearch_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        ClearSearchCriteria()
    End Sub
    Private Sub ClearSearchCriteria()

        Try
            SearchActiveFileTextBox.Text = String.Empty

            'Update Page State
            With Me.State
                .activefilename = SearchActiveFileTextBox.Text

            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub


    Protected Sub chkbxreset_checkedchanged(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles chkbxreset.CheckedChanged
        Dim row As Integer
        Dim resetchk As CheckBox
        Dim InterfaceStatusWrkId As Guid
        Dim interfaceStatusWrk As InterfaceStatusWrk
        Dim gv As GridViewRow = CType(CType(sender, Control).Parent.Parent, GridViewRow)
        row = gv.RowIndex
        resetchk = CType(Me.Grid.Rows(row).Cells(Me.GRID_COL_RESET_IDX).FindControl("chkbxreset"), CheckBox)
        InterfaceStatusWrkId = New Guid(Me.Grid.Rows(row).Cells(Me.GRID_COL_INTERFACE_STATUS_IDX).Text)
        If resetchk.Checked = True Then
            interfaceStatusWrk = New InterfaceStatusWrk(InterfaceStatusWrkId)
            Me.PopulateBOProperty(interfaceStatusWrk, STATUS_PROPERTY, "Failure")
            Me.PopulateBOProperty(interfaceStatusWrk, ACTIVE_FILENAME_PROPERTY, String.Empty)
            interfaceStatusWrk.Save()
            Me.State.HasDataChanged = True
            Me.PopulateGrid()
            Me.State.HasDataChanged = False

            'Grid.Rows(row).Cells(GRID_COL_ACTIVE_FILE_IDX).Text = " "
            'Grid.Rows(row).Cells(GRID_COL_STATUS_IDX).Text = "Failure"
            'ShowInfoMsgBox("CONTACT BA TO ROLLBACK THE FILE TO PREVIOUS STATUS", True)
        End If

    End Sub


#End Region


#Region "Helpers"

    Private Sub ShowInfoMsgBox(ByVal strMsg As String, Optional ByVal Translate As Boolean = True)
        Dim translatedMsg As String = strMsg
        If Translate Then translatedMsg = TranslationBase.TranslateLabelOrMessage(strMsg)
        Dim sJavaScript As String
        sJavaScript = "<SCRIPT>" & Environment.NewLine
        sJavaScript &= "setTimeout(""showMessage('" & translatedMsg & "', '" & "AlertWindow" & "', '" & Me.MSG_BTN_OK & "', '" & Me.MSG_TYPE_INFO & "', '" & "null" & "')"", 0);" & Environment.NewLine
        sJavaScript &= "</SCRIPT>" & Environment.NewLine
        Me.RegisterStartupScript("ShowConfirmation", sJavaScript)
    End Sub
#End Region




#Region "Error Handling"
#End Region

End Class
