Partial Class CommentListForm
    Inherits ElitaPlusSearchPage

    Protected WithEvents ErrorCtrl As ErrorController
    Protected WithEvents LabelTables As System.Web.UI.WebControls.Label


#Region " Web Form Designer Generated Code "

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
    Public Const GRID_COL_EDIT As Integer = 0
    Public Const GRID_COL_COMMENT_ID As Integer = 1
    Public Const GRID_COL_TIME_STAMP As Integer = 2
    Public Const GRID_COL_CALLER_NAME As Integer = 3
    Public Const GRID_COL_ADDED_BY As Integer = 4
    Public Const GRID_COL_COMMENT_TEXT As Integer = 5

    Public Const EXT_GRID_COL_COMMENT_ID As Integer = 0
    Public Const EXT_GRID_COL_TIME_STAMP As Integer = 1
    Public Const EXT_GRID_COL_CALLER_NAME As Integer = 2
    Public Const EXT_GRID_COL_ADDED_BY As Integer = 3
    Public Const EXT_GRID_COL_COMMENT_TEXT As Integer = 4
#End Region

    Public Class Parameters
        Public CertId As Guid
        Public ClaimId As Guid
        Public Sub New(certId As Guid, Optional ByVal claimId As Object = Nothing)
            Me.CertId = certId
            If claimId IsNot Nothing Then
                Me.ClaimId = CType(claimId, Guid)
            End If
        End Sub
    End Class

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public PageIndex As Integer = 0
        Public SortExpression As String = Comment.CommentSearchDV.COL_CREATED_DATE & " DESC"
        Public SelectedCommentId As Guid = Guid.Empty
        Public IsGridVisible As Boolean = True
        Public SearchCode As String = ""
        Public SearchDescription As String = ""
        Public InputParameters As Parameters

        Sub New()
        End Sub
    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            If NavController.State Is Nothing Then
                NavController.State = New MyState
                Me.State.InputParameters = CType(NavController.ParametersPassed, Parameters)
            End If
            Return CType(NavController.State, MyState)
        End Get
    End Property

    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Try
            MenuEnabled = True
            IsReturningFromChild = True
            Dim retObj As CommentForm.ReturnType = CType(ReturnPar, CommentForm.ReturnType)
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If retObj IsNot Nothing Then
                        If Not retObj.EditingBo.IsNew Then
                            State.SelectedCommentId = retObj.EditingBo.Id
                        End If
                        State.IsGridVisible = True
                    End If
                Case ElitaPlusPage.DetailPageCommand.Delete
                    DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
            End Select
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub SaveGuiState()
    End Sub

    Private Sub RestoreGuiState()
    End Sub

#End Region

#Region "Page_Events"
    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ErrorCtrl.Clear_Hide()
        'If Me.NavController.PrevNavState.Name = "CLAIM_DETAIL" Then
        '    Me.LabelTables.Text = Me.TranslateLabelOrMessage("CLAIMS")
        'End If
        Try
            If Not IsPostBack Then
                RestoreGuiState()
                Trace(Me, "Cert Id =" & GuidControl.GuidToHexString(State.InputParameters.CertId))
                If State.IsGridVisible Then
                    PopulateGrid()
                    PopulateExtGrid()
                End If
                SetGridItemStyleColor(Grid)
                SetGridItemStyleColor(ExtGrid)
            Else
                SaveGuiState()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
        ShowMissingTranslations(ErrorCtrl)
    End Sub
#End Region

#Region "Controlling Logic"

    Public Sub PopulateGrid()
        Try
            Dim cert As New Certificate(State.InputParameters.CertId)
            TextboxCertificate.Text = cert.CertNumber
            Dim dv As Comment.CommentSearchDV = Comment.getList(State.InputParameters.CertId)
            dv.Sort = State.SortExpression

            Grid.AutoGenerateColumns = False
            Grid.Columns(GRID_COL_ADDED_BY).SortExpression = Comment.CommentSearchDV.COL_ADDED_BY
            Grid.Columns(GRID_COL_CALLER_NAME).SortExpression = Comment.CommentSearchDV.COL_CALLER_NAME
            Grid.Columns(GRID_COL_TIME_STAMP).SortExpression = Comment.CommentSearchDV.COL_CREATED_DATE

            SetPageAndSelectedIndexFromGuid(dv, State.SelectedCommentId, Grid, State.PageIndex)
            State.PageIndex = Grid.CurrentPageIndex
            Grid.DataSource = dv
            Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)

            'Dim i As Integer
            'For i = 0 To Grid.Columns.Count - 1
            '    Grid.Columns.Item(3)..Attributes.Add("OnClick", "return ValidateInsertUpdate();")
            'Next
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Public Sub PopulateExtGrid()
        Try
            Dim dv As Comment.ExtCommentSearchDV = Comment.getExtList(State.InputParameters.ClaimId)
            dv.Sort = State.SortExpression

            ExtGrid.AutoGenerateColumns = False
            ExtGrid.Columns(EXT_GRID_COL_ADDED_BY).SortExpression = Comment.ExtCommentSearchDV.COL_EXT_ADDED_BY
            ExtGrid.Columns(EXT_GRID_COL_CALLER_NAME).SortExpression = Comment.ExtCommentSearchDV.COL_EXT_CALLER_NAME
            ExtGrid.Columns(EXT_GRID_COL_TIME_STAMP).SortExpression = Comment.ExtCommentSearchDV.COL_EXT_CREATED_DATE

            SetPageAndSelectedIndexFromGuid(dv, State.SelectedCommentId, ExtGrid, State.PageIndex)
            State.PageIndex = ExtGrid.CurrentPageIndex
            ExtGrid.DataSource = dv
            ExtGrid.DataBind()

            Dim str As String = dv.Table.Columns(1).ToString()
            ControlMgr.SetVisibleControl(Me, ExtGrid, State.IsGridVisible)

            'Dim i As Integer
            'For i = 0 To Grid.Columns.Count - 1
            '    Grid.Columns.Item(3)..Attributes.Add("OnClick", "return ValidateInsertUpdate();")
            'Next
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub




#End Region


#Region " Datagrid Related "

    'The Binding LOgic is here  
    Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
        Try
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

            If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                e.Item.Cells(GRID_COL_COMMENT_ID).Text = New Guid(CType(dvRow(Comment.CommentSearchDV.COL_COMMENT_ID), Byte())).ToString
                e.Item.Cells(GRID_COL_ADDED_BY).Text = dvRow(Comment.CommentSearchDV.COL_ADDED_BY).ToString
                e.Item.Cells(GRID_COL_CALLER_NAME).Text = dvRow(Comment.CommentSearchDV.COL_CALLER_NAME).ToString
                e.Item.Cells(GRID_COL_COMMENT_TEXT).Text = dvRow(Comment.CommentSearchDV.COL_COMMENTS).ToString
                Dim createdDate As Date = CType(dvRow(Comment.CommentSearchDV.COL_CREATED_DATE), Date)
                e.Item.Cells(GRID_COL_TIME_STAMP).Text = GetLongDateFormattedString(createdDate)
            End If

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
            State.SelectedCommentId = Nothing
            State.PageIndex = 0
            PopulateGrid()
            PopulateExtGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Public Sub ItemCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Try
            If e.CommandName = "SelectAction" Then
                State.SelectedCommentId = New Guid(e.Item.Cells(GRID_COL_COMMENT_ID).Text)
                Dim originalComment As Comment = New Comment(State.SelectedCommentId)
                Dim newComment As Comment = Comment.GetNewComment(originalComment)
                NavController.Navigate(Me, FlowEvents.EVENT_COMMENT_SELECTED, New CommentForm.Parameters(newComment))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

    Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.DataGridItemEventArgs)
        BaseItemCreated(sender, e)
        ''-------------------------------------
        ''Name:ReasorbTranslation
        ''Purpose:Translate any message tobe display
        ''Input Values: Message
        ''Uses:
        ''-------------------------------------
        '' get the type of item being created
        'Dim elemType As ListItemType = e.Item.ItemType

        '' make sure it is the pager bar
        'If elemType = ListItemType.Pager Then
        '    ' the pager bar as a whole has the follwoing layout
        '    ' <TR><TD colspan=x>.....links</TD></TR>
        '    ' item points to <TR>. The code below moves to <TD>
        '    Dim pager As TableCell = DirectCast(e.Item.Controls(0), TableCell)
        '    Dim i As Int32 = 0
        '    Dim bFound As Boolean = False
        '    For i = 0 To pager.Controls.Count
        '        Dim obj As Object = pager.Controls(i)

        '        If obj.GetType.ToString = "System.Web.UI.WebControls.DataGridLinkButton" Then

        '            Dim h As LinkButton = CType(obj, LinkButton)
        '            'h.Text = "[" & h.Text & "]"

        '            If h.Text.Equals("...") Then
        '                If Not bFound Then
        '                    'h.Text = "<"
        '                    'h.Text = ".        .."
        '                    h.ToolTip = "Previous set of pages"
        '                    h.Style.Add("BACKGROUND-REPEAT", "no-repeat")
        '                    h.Style.Add("COLOR", "#dee3e7")
        '                    h.Style.Add("BACKGROUND-IMAGE", "url(../Navigation/images/arrow_back.gif)")
        '                    bFound = True
        '                Else
        '                    'h.Text = ">"
        '                    h.ToolTip = "Next set of pages"
        '                    h.Text = ".        .."
        '                    h.Style.Add("COLOR", "#dee3e7")
        '                    h.Style.Add("BACKGROUND-REPEAT", "no-repeat")
        '                    h.Style.Add("BACKGROUND-IMAGE", "url(../Navigation/images/arrow_foward.gif)")
        '                End If

        '            End If

        '        Else
        '            bFound = True
        '            Dim l As System.Web.UI.WebControls.Label = CType(obj, System.Web.UI.WebControls.Label)
        '            l.Text = "Page" & l.Text
        '            'l.ForeColor = Color.Black
        '            l.Style.Add("FONT-WEIGHT", "BOLD")
        '        End If

        '        i += 1
        '    Next
        'Else
        '    'If elemType = ListItemType.AlternatingItem Then
        '    '    Dim objButton As Button


        '    '    objButton = DirectCast(e.Item.Cells(5).Controls(0), Button)
        '    '    objButton.Style.Add("background-color", "#dee3e7")
        '    '    objButton.Style.Add("cursor", "hand")
        '    '    objButton.CssClass = "FLATBUTTON"


        '    'ElseIf elemType = ListItemType.Item Then
        '    '    Dim objButton As Button


        '    '    objButton = DirectCast(e.Item.Cells(5).Controls(0), Button)
        '    '    objButton.Style.Add("background-color", "#dee3e7")
        '    '    objButton.Style.Add("cursor", "hand")
        '    '    objButton.CssClass = "FLATBUTTON"

        '    'End If
        'End If
    End Sub

    Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = e.NewPageIndex
            State.SelectedCommentId = Guid.Empty
            PopulateGrid()
            PopulateExtGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub


#End Region

#Region " Buttons Clicks "

    Private Sub btnAdd_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd_WRITE.Click
        Try
            NavController.Navigate(Me, FlowEvents.EVENT_COMMENT_SELECTED, New CommentForm.Parameters(Comment.GetNewComment(State.InputParameters.CertId, State.InputParameters.ClaimId)))
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            NavController.Navigate(Me, FlowEvents.EVENT_BACK)
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub
#End Region

#Region "Error Handling"


#End Region




End Class
