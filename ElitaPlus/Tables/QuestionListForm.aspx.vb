Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Tables

    'Purpose:               Handle value change on ddlQuestionType
    'Author:                Arnie Lugo
    'Date:                  03/14/2012
    'Modification History:  REQ-860

    Partial Public Class QuestionListForm
        Inherits ElitaPlusSearchPage

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
        Private Const GRID_COL_EDIT As Integer = 0
        Private Const GRID_COL_SOFT_QUESTION_ID_IDX As Integer = 1
        Private Const GRID_COL_CODE_IDX As Integer = 2
        Private Const GRID_COL_DESCRIPTION_IDX As Integer = 3
        Private Const GRID_COL_QUESTION_TYPE_IDX As Integer = 4
        Private Const GRID_COL_EFFECTIVE_DATE_IDX As Integer = 5
        Private Const GRID_COL_EXPIRATION_DATE_IDX As Integer = 6
#End Region

#Region "Page State"
        Private IsReturningFromChild As Boolean = False

        Class MyState
            Public SelectedSoftQuestionId As Guid = Guid.Empty
            Public Code As String = String.Empty
            Public Description As String = String.Empty
            Public Questiontype As String = String.Empty
            Public SearchTags As String = String.Empty
            Public Issue As String = String.Empty
            Public ActiveOn As String = DateTime.Now().ToString("dd-MMM-yyyy") 'String.Empty
            Public Effective As String = String.Empty
            Public Expiration As String = String.Empty
            Public SortExpression As String = Question.QuestionSearchDV.COL_NAME_CODE
            Public PageIndex As Integer
            Public SearchDV As Question.QuestionSearchDV
            Public HasDataChanged As Boolean
            Public IsGridVisible As Boolean
            Public SelectedPageSize As Integer

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

        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            Try
                MenuEnabled = True
                IsReturningFromChild = True
                Dim retObj As QuestionForm.ReturnType = CType(ReturnPar, QuestionForm.ReturnType)
                State.HasDataChanged = retObj.HasDataChanged
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If retObj IsNot Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                State.SelectedSoftQuestionId = retObj.EditingBo.Id
                            End If
                            State.IsGridVisible = True
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Expire
                        DisplayMessage(Message.EXPIRE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                End Select
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub SaveGuiState()
            With State
                .Code = txtCode.Text
                .Description = txtDescription.Text
                .Questiontype = GetSelectedDescription(ddlQuestionType)
                .SearchTags = txtSearchTags.Text
                .Issue = txtIssue.Text
                .ActiveOn = txtActiveOn.Text
            End With
        End Sub

        Private Sub RestoreGuiState()
            With State
                txtCode.Text = .Code
                txtDescription.Text = .Description
                SetSelectedItemByText(ddlQuestionType, .Questiontype)
                txtSearchTags.Text = .SearchTags
                txtIssue.Text = .Issue
                txtActiveOn.Text = .ActiveOn
            End With
        End Sub

#End Region

#Region "Page_Events"
        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                ErrorCtrl.Clear_Hide()
                If Not IsPostBack Then
                    SetDefaultButton(txtCode, btnSearch)
                    SetDefaultButton(txtDescription, btnSearch)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    ControlMgr.SetEnableControl(Me, txtIssue, False)
                    AddCalendar(imgActiveOn, txtActiveOn)
                    PopulateDropDowns()
                    RestoreGuiState()
                    If State.IsGridVisible Then
                        If Not (State.SelectedPageSize = DEFAULT_PAGE_SIZE) Then
                            cboPageSize.SelectedValue = CType(State.SelectedPageSize, String)
                            If (State.SelectedPageSize = 0) Then
                                State.SelectedPageSize = DEFAULT_PAGE_SIZE
                            End If
                            Grid.PageSize = State.SelectedPageSize
                        End If
                        PopulateGrid()
                    End If
                    SetGridItemStyleColor(Grid)
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

        Private Sub PopulateDropDowns()
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            ' Me.BindListControlToDataView(ddlQuestionType, LookupListNew.GetQuestionTypeLookupList(langId)) 'QTYP
            ddlQuestionType.Populate(CommonConfigManager.Current.ListManager.GetList("QTYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                           {
                 .AddBlankItem = True
                   })

        End Sub

        Public Sub PopulateGrid()

            Dim chkDteActiveOn As DateTime
            Dim dteActiveOn As DateTime = Nothing
            Dim strActiveOn As String = String.Empty


            If Date.TryParse(txtActiveOn.Text, chkDteActiveOn) Then
                dteActiveOn = chkDteActiveOn
                strActiveOn = dteActiveOn.ToString("dd-MMM-yyyy")
            End If

            If ((State.SearchDV Is Nothing) OrElse (State.HasDataChanged)) Then
                State.SearchDV = Question.getQuestionList(txtCode.Text, txtDescription.Text, New Guid(ddlQuestionType.SelectedItem.Value), txtSearchTags.Text,
                    txtIssue.Text, strActiveOn)
            End If
            State.SearchDV.Sort = State.SortExpression

            Grid.AutoGenerateColumns = False
            Grid.Columns(GRID_COL_CODE_IDX).SortExpression = Question.QuestionSearchDV.COL_NAME_CODE
            Grid.Columns(GRID_COL_DESCRIPTION_IDX).SortExpression = Question.QuestionSearchDV.COL_NAME_DESCRIPTION
            Grid.Columns(GRID_COL_QUESTION_TYPE_IDX).SortExpression = Question.QuestionSearchDV.COL_NAME_QUESTION_TYPE
            Grid.Columns(GRID_COL_EFFECTIVE_DATE_IDX).SortExpression = Question.QuestionSearchDV.COL_NAME_EFFECTIVE
            Grid.Columns(GRID_COL_EXPIRATION_DATE_IDX).SortExpression = Question.QuestionSearchDV.COL_NAME_EXPIRATION

            SetPageAndSelectedIndexFromGuid(State.SearchDV, State.SelectedSoftQuestionId, Grid, State.PageIndex)
            SortAndBindGrid()
        End Sub

        Private Sub SortAndBindGrid()
            State.PageIndex = Grid.CurrentPageIndex
            Grid.DataSource = State.SearchDV
            HighLightSortColumn(Grid, State.SortExpression)
            Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)

            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

            Session("recCount") = State.SearchDV.Count

            If State.SearchDV.Count > 0 Then

                If Grid.Visible Then
                    lblRecordCount.Text = State.SearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            Else
                If Grid.Visible Then
                    lblRecordCount.Text = State.SearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
        End Sub
#End Region

#Region " Datagrid Related "

        'The Binding Logic is here  
        Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
            Try
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    e.Item.Cells(GRID_COL_SOFT_QUESTION_ID_IDX).Text = New Guid(CType(dvRow(Question.QuestionSearchDV.COL_NAME_SOFT_QUESTION_ID), Byte())).ToString
                    e.Item.Cells(GRID_COL_CODE_IDX).Text = dvRow(Question.QuestionSearchDV.COL_NAME_CODE).ToString
                    e.Item.Cells(GRID_COL_DESCRIPTION_IDX).Text = dvRow(Question.QuestionSearchDV.COL_NAME_DESCRIPTION).ToString
                    e.Item.Cells(GRID_COL_QUESTION_TYPE_IDX).Text = dvRow(Question.QuestionSearchDV.COL_NAME_QUESTION_TYPE).ToString
                    e.Item.Cells(GRID_COL_EFFECTIVE_DATE_IDX).Text = dvRow(Question.QuestionSearchDV.COL_NAME_EFFECTIVE).ToString
                    e.Item.Cells(GRID_COL_EXPIRATION_DATE_IDX).Text = dvRow(Question.QuestionSearchDV.COL_NAME_EXPIRATION).ToString
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
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

        Public Sub ItemCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)
            Try
                If e.CommandName = "SelectAction" Then
                    State.SelectedSoftQuestionId = New Guid(e.Item.Cells(GRID_COL_SOFT_QUESTION_ID_IDX).Text)
                    callPage(QuestionForm.URL, State.SelectedSoftQuestionId)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try

        End Sub

        Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_PageIndexChanged(source As System.Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
            Try
                State.PageIndex = e.NewPageIndex
                State.SelectedSoftQuestionId = Guid.Empty
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Button Clicks "

        Protected Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
            Try
                State.PageIndex = 0
                State.SelectedSoftQuestionId = Guid.Empty
                State.IsGridVisible = True
                State.SearchDV = Nothing
                State.HasDataChanged = False
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Protected Sub btnAdd_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd_WRITE.Click
            Try
                callPage(QuestionForm.URL)
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Protected Sub btnClearSearch_Click(sender As Object, e As System.EventArgs) Handles btnClearSearch.Click
            Try
                txtCode.Text = ""
                txtDescription.Text = ""
                txtSearchTags.Text = ""
                ddlQuestionType.SelectedIndex = BLANK_ITEM_SELECTED
                txtIssue.Text = ""
                txtActiveOn.Text = ""
                txtIssue.Enabled = False
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Error Handling"


#End Region



        Protected Sub ddlQuestionType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlQuestionType.SelectedIndexChanged
            If ddlQuestionType.SelectedItem.Text.ToUpper() = "ISSUE" Then
                ControlMgr.SetEnableControl(Me, txtIssue, True)
            Else
                txtIssue.Text = String.Empty
                ControlMgr.SetEnableControl(Me, txtIssue, False)
            End If
        End Sub
    End Class

End Namespace
