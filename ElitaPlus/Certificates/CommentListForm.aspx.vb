Partial Class CommentListForm
    Inherits ElitaPlusSearchPage

    Protected WithEvents ErrorCtrl As ErrorController
    Protected WithEvents LabelTables As System.Web.UI.WebControls.Label


#Region " Web Form Designer Generated Code "

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
        Public Sub New(ByVal certId As Guid, Optional ByVal claimId As Object = Nothing)
            Me.CertId = certId
            If Not claimId Is Nothing Then
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
            If Me.NavController.State Is Nothing Then
                Me.NavController.State = New MyState
                Me.State.InputParameters = CType(Me.NavController.ParametersPassed, Parameters)
            End If
            Return CType(Me.NavController.State, MyState)
        End Get
    End Property

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            Me.MenuEnabled = True
            Me.IsReturningFromChild = True
            Dim retObj As CommentForm.ReturnType = CType(ReturnPar, CommentForm.ReturnType)
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If Not retObj Is Nothing Then
                        If Not retObj.EditingBo.IsNew Then
                            Me.State.SelectedCommentId = retObj.EditingBo.Id
                        End If
                        Me.State.IsGridVisible = True
                    End If
                Case ElitaPlusPage.DetailPageCommand.Delete
                    Me.DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
            End Select
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub SaveGuiState()
    End Sub

    Private Sub RestoreGuiState()
    End Sub

#End Region

#Region "Page_Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Me.ErrorCtrl.Clear_Hide()
        'If Me.NavController.PrevNavState.Name = "CLAIM_DETAIL" Then
        '    Me.LabelTables.Text = Me.TranslateLabelOrMessage("CLAIMS")
        'End If
        Try
            If Not Me.IsPostBack Then
                Me.RestoreGuiState()
                Trace(Me, "Cert Id =" & GuidControl.GuidToHexString(Me.State.InputParameters.CertId))
                If Me.State.IsGridVisible Then
                    Me.PopulateGrid()
                    Me.PopulateExtGrid()
                End If
                Me.SetGridItemStyleColor(Me.Grid)
                Me.SetGridItemStyleColor(Me.ExtGrid)
            Else
                Me.SaveGuiState()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
        Me.ShowMissingTranslations(Me.ErrorCtrl)
    End Sub
#End Region

#Region "Controlling Logic"

    Public Sub PopulateGrid()
        Try
            Dim cert As New Certificate(Me.State.InputParameters.CertId)
            Me.TextboxCertificate.Text = cert.CertNumber
            Dim dv As Comment.CommentSearchDV = Comment.getList(Me.State.InputParameters.CertId)
            dv.Sort = Me.State.SortExpression

            Me.Grid.AutoGenerateColumns = False
            Me.Grid.Columns(Me.GRID_COL_ADDED_BY).SortExpression = Comment.CommentSearchDV.COL_ADDED_BY
            Me.Grid.Columns(Me.GRID_COL_CALLER_NAME).SortExpression = Comment.CommentSearchDV.COL_CALLER_NAME
            Me.Grid.Columns(Me.GRID_COL_TIME_STAMP).SortExpression = Comment.CommentSearchDV.COL_CREATED_DATE

            SetPageAndSelectedIndexFromGuid(dv, Me.State.SelectedCommentId, Me.Grid, Me.State.PageIndex)
            Me.State.PageIndex = Me.Grid.CurrentPageIndex
            Me.Grid.DataSource = dv
            Me.Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)

            'Dim i As Integer
            'For i = 0 To Grid.Columns.Count - 1
            '    Grid.Columns.Item(3)..Attributes.Add("OnClick", "return ValidateInsertUpdate();")
            'Next
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Public Sub PopulateExtGrid()
        Try
            Dim dv As Comment.ExtCommentSearchDV = Comment.getExtList(Me.State.InputParameters.ClaimId)
            dv.Sort = Me.State.SortExpression

            Me.ExtGrid.AutoGenerateColumns = False
            Me.ExtGrid.Columns(Me.EXT_GRID_COL_ADDED_BY).SortExpression = Comment.ExtCommentSearchDV.COL_EXT_ADDED_BY
            Me.ExtGrid.Columns(Me.EXT_GRID_COL_CALLER_NAME).SortExpression = Comment.ExtCommentSearchDV.COL_EXT_CALLER_NAME
            Me.ExtGrid.Columns(Me.EXT_GRID_COL_TIME_STAMP).SortExpression = Comment.ExtCommentSearchDV.COL_EXT_CREATED_DATE

            SetPageAndSelectedIndexFromGuid(dv, Me.State.SelectedCommentId, Me.ExtGrid, Me.State.PageIndex)
            Me.State.PageIndex = Me.ExtGrid.CurrentPageIndex
            Me.ExtGrid.DataSource = dv
            Me.ExtGrid.DataBind()

            Dim str As String = dv.Table.Columns(1).ToString()
            ControlMgr.SetVisibleControl(Me, ExtGrid, Me.State.IsGridVisible)

            'Dim i As Integer
            'For i = 0 To Grid.Columns.Count - 1
            '    Grid.Columns.Item(3)..Attributes.Add("OnClick", "return ValidateInsertUpdate();")
            'Next
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub




#End Region


#Region " Datagrid Related "

    'The Binding LOgic is here  
    Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
        Try
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                e.Item.Cells(Me.GRID_COL_COMMENT_ID).Text = New Guid(CType(dvRow(Comment.CommentSearchDV.COL_COMMENT_ID), Byte())).ToString
                e.Item.Cells(Me.GRID_COL_ADDED_BY).Text = dvRow(Comment.CommentSearchDV.COL_ADDED_BY).ToString
                e.Item.Cells(Me.GRID_COL_CALLER_NAME).Text = dvRow(Comment.CommentSearchDV.COL_CALLER_NAME).ToString
                e.Item.Cells(Me.GRID_COL_COMMENT_TEXT).Text = dvRow(Comment.CommentSearchDV.COL_COMMENTS).ToString
                'If dvRow(Comment.CommentSearchDV.COL_COMMENTS).ToString.Length > 85 Then
                '    e.Item.Cells(Me.GRID_COL_COMMENT_TEXT).Text = dvRow(Comment.CommentSearchDV.COL_COMMENTS).ToString.Substring(0, 85)
                'Else
                '    e.Item.Cells(Me.GRID_COL_COMMENT_TEXT).Text = dvRow(Comment.CommentSearchDV.COL_COMMENTS).ToString
                'End If
                Dim createdDate As Date = CType(dvRow(Comment.CommentSearchDV.COL_CREATED_DATE), Date)
                e.Item.Cells(Me.GRID_COL_TIME_STAMP).Text = Me.GetLongDateFormattedString(createdDate)
            End If

            '''e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='#99CCFF'")

            '''e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'")
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub Grid_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles Grid.SortCommand
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
            Me.State.SelectedCommentId = Nothing
            Me.State.PageIndex = 0
            Me.PopulateGrid()
            Me.PopulateExtGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Public Sub ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Try
            If e.CommandName = "SelectAction" Then
                Me.State.SelectedCommentId = New Guid(e.Item.Cells(Me.GRID_COL_COMMENT_ID).Text)
                Dim originalComment As Comment = New Comment(Me.State.SelectedCommentId)
                Dim newComment As Comment = Comment.GetNewComment(originalComment)
                Me.NavController.Navigate(Me, FlowEvents.EVENT_COMMENT_SELECTED, New CommentForm.Parameters(newComment))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

    Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
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

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.State.SelectedCommentId = Guid.Empty
            Me.PopulateGrid()
            Me.PopulateExtGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub


#End Region

#Region " Buttons Clicks "

    Private Sub btnAdd_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd_WRITE.Click
        Try
            Me.NavController.Navigate(Me, FlowEvents.EVENT_COMMENT_SELECTED, New CommentForm.Parameters(Comment.GetNewComment(Me.State.InputParameters.CertId, Me.State.InputParameters.ClaimId)))
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.NavController.Navigate(Me, FlowEvents.EVENT_BACK)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub
#End Region

#Region "Error Handling"


#End Region




End Class
