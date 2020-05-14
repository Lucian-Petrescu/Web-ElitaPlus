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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As EquipmentList
            Public HasDataChanged As Boolean
            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As EquipmentList, ByVal hasDataChanged As Boolean)
                Me.LastOperation = LastOp
                Me.EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
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

        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            Try
                Me.MenuEnabled = True
                Me.IsReturningFromChild = True
                Dim retObj As QuestionListForm.ReturnType = CType(ReturnPar, QuestionListForm.ReturnType)
                Me.State.HasDataChanged = retObj.HasDataChanged
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If Not retObj Is Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                Me.State.SelectedSoftQuestionId = retObj.EditingBo.Id
                            End If
                            Me.State.IsGridVisible = True
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Expire
                        Me.DisplayMessage(Message.EXPIRE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        Me.DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                End Select
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub SaveGuiState()
            With Me.State
                .Code = Me.txtCode.Text
                .Description = Me.txtDescription.Text
                .Questiontype = Me.GetSelectedDescription(ddlQuestionType)
                .SearchTags = Me.txtSearchTags.Text
                .Issue = Me.txtIssue.Text
                .ActiveOn = Me.txtActiveOn.Text
            End With
        End Sub

        Private Sub RestoreGuiState()
            With Me.State
                Me.txtCode.Text = .Code
                Me.txtDescription.Text = .Description
                Me.SetSelectedItemByText(ddlQuestionType, .Questiontype)
                Me.txtSearchTags.Text = .SearchTags
                Me.txtIssue.Text = .Issue
                Me.txtActiveOn.Text = .ActiveOn
            End With
        End Sub

#End Region

#Region "Page_Events"
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                Me.ErrorCtrl.Clear_Hide()
                If Not Me.IsPostBack Then
                    Me.SetDefaultButton(Me.txtCode, btnSearch)
                    Me.SetDefaultButton(Me.txtDescription, btnSearch)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    ControlMgr.SetEnableControl(Me, txtIssue, False)
                    Me.AddCalendar(Me.imgActiveOn, Me.txtActiveOn)
                    PopulateDropDowns()
                    Me.RestoreGuiState()
                    If Me.State.IsGridVisible Then
                        If Not (Me.State.SelectedPageSize = DEFAULT_PAGE_SIZE) Then
                            cboPageSize.SelectedValue = CType(Me.State.SelectedPageSize, String)
                            If (Me.State.SelectedPageSize = 0) Then
                                Me.State.SelectedPageSize = DEFAULT_PAGE_SIZE
                            End If
                            Grid.PageSize = Me.State.SelectedPageSize
                        End If
                        Me.PopulateGrid()
                    End If
                    Me.SetGridItemStyleColor(Me.Grid)
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

            If ((Me.State.SearchDV Is Nothing) OrElse (Me.State.HasDataChanged)) Then
                Me.State.SearchDV = Question.getQuestionList(Me.txtCode.Text, Me.txtDescription.Text, New Guid(Me.ddlQuestionType.SelectedItem.Value), Me.txtSearchTags.Text,
                    Me.txtIssue.Text, strActiveOn)
            End If
            Me.State.SearchDV.Sort = Me.State.SortExpression

            Me.Grid.AutoGenerateColumns = False
            Me.Grid.Columns(Me.GRID_COL_CODE_IDX).SortExpression = Question.QuestionSearchDV.COL_NAME_CODE
            Me.Grid.Columns(Me.GRID_COL_DESCRIPTION_IDX).SortExpression = Question.QuestionSearchDV.COL_NAME_DESCRIPTION
            Me.Grid.Columns(Me.GRID_COL_QUESTION_TYPE_IDX).SortExpression = Question.QuestionSearchDV.COL_NAME_QUESTION_TYPE
            Me.Grid.Columns(Me.GRID_COL_EFFECTIVE_DATE_IDX).SortExpression = Question.QuestionSearchDV.COL_NAME_EFFECTIVE
            Me.Grid.Columns(Me.GRID_COL_EXPIRATION_DATE_IDX).SortExpression = Question.QuestionSearchDV.COL_NAME_EXPIRATION

            SetPageAndSelectedIndexFromGuid(Me.State.SearchDV, Me.State.SelectedSoftQuestionId, Me.Grid, Me.State.PageIndex)
            Me.SortAndBindGrid()
        End Sub

        Private Sub SortAndBindGrid()
            Me.State.PageIndex = Me.Grid.CurrentPageIndex
            Me.Grid.DataSource = Me.State.SearchDV
            HighLightSortColumn(Grid, Me.State.SortExpression)
            Me.Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)

            ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

            Session("recCount") = Me.State.SearchDV.Count

            If Me.State.SearchDV.Count > 0 Then

                If Me.Grid.Visible Then
                    Me.lblRecordCount.Text = Me.State.SearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            Else
                If Me.Grid.Visible Then
                    Me.lblRecordCount.Text = Me.State.SearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
        End Sub
#End Region

#Region " Datagrid Related "

        'The Binding Logic is here  
        Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
            Try
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    e.Item.Cells(Me.GRID_COL_SOFT_QUESTION_ID_IDX).Text = New Guid(CType(dvRow(Question.QuestionSearchDV.COL_NAME_SOFT_QUESTION_ID), Byte())).ToString
                    e.Item.Cells(Me.GRID_COL_CODE_IDX).Text = dvRow(Question.QuestionSearchDV.COL_NAME_CODE).ToString
                    e.Item.Cells(Me.GRID_COL_DESCRIPTION_IDX).Text = dvRow(Question.QuestionSearchDV.COL_NAME_DESCRIPTION).ToString
                    e.Item.Cells(Me.GRID_COL_QUESTION_TYPE_IDX).Text = dvRow(Question.QuestionSearchDV.COL_NAME_QUESTION_TYPE).ToString
                    e.Item.Cells(Me.GRID_COL_EFFECTIVE_DATE_IDX).Text = dvRow(Question.QuestionSearchDV.COL_NAME_EFFECTIVE).ToString
                    e.Item.Cells(Me.GRID_COL_EXPIRATION_DATE_IDX).Text = dvRow(Question.QuestionSearchDV.COL_NAME_EXPIRATION).ToString
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.PopulateGrid()
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
                Me.State.PageIndex = 0
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Public Sub ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
            Try
                If e.CommandName = "SelectAction" Then
                    Me.State.SelectedSoftQuestionId = New Guid(e.Item.Cells(Me.GRID_COL_SOFT_QUESTION_ID_IDX).Text)
                    Me.callPage(QuestionForm.URL, Me.State.SelectedSoftQuestionId)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try

        End Sub

        Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
            Try
                Me.State.PageIndex = e.NewPageIndex
                Me.State.SelectedSoftQuestionId = Guid.Empty
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Button Clicks "

        Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Try
                Me.State.PageIndex = 0
                Me.State.SelectedSoftQuestionId = Guid.Empty
                Me.State.IsGridVisible = True
                Me.State.SearchDV = Nothing
                Me.State.HasDataChanged = False
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Protected Sub btnAdd_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd_WRITE.Click
            Try
                Me.callPage(QuestionForm.URL)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Protected Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
            Try
                Me.txtCode.Text = ""
                Me.txtDescription.Text = ""
                Me.txtSearchTags.Text = ""
                Me.ddlQuestionType.SelectedIndex = Me.BLANK_ITEM_SELECTED
                Me.txtIssue.Text = ""
                Me.txtActiveOn.Text = ""
                Me.txtIssue.Enabled = False
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
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
